using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MazeRunner.Contracts;
using MazeRunner.Utils;

namespace MazeRunner.TestbedUI.Controls;

public partial class CCMazeCanvas : UserControl
{
    private const int CellEdgeLength = 80;

    private RowStyle StandardRowStyle => new(SizeType.Absolute, height: CellEdgeLength);
    private ColumnStyle StandardColumnStyle => new(SizeType.Absolute, width: CellEdgeLength);

    private IMaze _maze;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IMaze Maze
    {
        set
        {
            if (_maze == value) return;

            _maze = value; //order
            Reinitialize(); //order
        }
        get => _maze;
    }

    public CCMazeCanvas()
    {
        InitializeComponent();

        //tlpMesh.SuspendDrawing() SuspendLayout() //todo  experiment with this technique to speed up the drawing process

        tlpMesh.RowStyles.Cast<RowStyle>().ForEach(x =>
        {
            x.Height = CellEdgeLength;
            x.SizeType = SizeType.Absolute;
        });
        tlpMesh.ColumnStyles.Cast<ColumnStyle>().ForEach(x =>
        {
            x.Width = CellEdgeLength;
            x.SizeType = SizeType.Absolute;
        });
    }

    public CCMazeCanvas CustomizeCell(Point cellCoords, Color backcolor, string textToAppend = null)
    {
        if (!_maze.Contains(cellCoords)) throw new ArgumentOutOfRangeException(nameof(cellCoords));

        //tlpMesh.SuspendDrawing() //todo  experiment with this technique to speed up the drawing process

        var add = false;
        var label = tlpMesh.GetControlFromPosition(column: cellCoords.X, row: cellCoords.Y);
        if (label == null)
        {
            add = true;
            label = SpawnControlForCell(backcolor: backcolor, font: FontForSimpleCells, fontcolor: White, text: "");
        }
        else
        {
            // label.Font = FontForSimpleCells;
            label.BackColor = _maze.HitTest(cellCoords) switch
            {
                MazeHitTestEnum.Free => backcolor,
                _ => label.BackColor // roadblocks, entrance and exit point get to keep their colors
            };
        }

        if (!string.IsNullOrEmpty(textToAppend))
        {
            label.Text += $@"{(string.IsNullOrEmpty(label.Text) ? "" : (label.Text == StartTag || label.Text == ExitTag ? " " : ", "))}{textToAppend}";
        }

        if (add)
        {
            tlpMesh.Controls.Add(label, column: cellCoords.X, row: cellCoords.Y); //1
        }

        return this;
    }
    //0 we need to force a redraw only of a specific cell in the tlp   thus we calculate its client rectangle and invoke invalidate on it followed by update
    //1 as an optimization we add the control deadlast after we have set its attributes   this is done because if the control gets added and then its properties
    //  get tweaked it might cause additional invalidated events to be triggered

    private void Reinitialize()
    {
        try
        {
            tlpMesh.SuspendDrawing().SuspendLayout(); //0 suspenddrawing
            Controls.Remove(tlpMesh);

            if (tlpMesh.ColumnStyles.Count > _maze.Size.Width || tlpMesh.RowStyles.Count > _maze.Size.Height)
            {
                tlpMesh.Controls.Clear(); //the only quick and safe way to drop labels in squares which are bound to be cut off
            }

            while (tlpMesh.ColumnStyles.Count != _maze.Size.Width)
            {
                if (tlpMesh.ColumnStyles.Count < _maze.Size.Width)
                {
                    tlpMesh.ColumnStyles.Add(StandardColumnStyle);
                }
                else //>
                {
                    tlpMesh.ColumnStyles.RemoveAt(tlpMesh.ColumnStyles.Count - 1);
                }
            }

            tlpMesh.ColumnCount = _maze.Size.Width;

            while (tlpMesh.RowStyles.Count != _maze.Size.Height)
            {
                if (tlpMesh.RowStyles.Count < _maze.Size.Height)
                {
                    tlpMesh.RowStyles.Add(StandardRowStyle);
                }
                else //>
                {
                    tlpMesh.RowStyles.RemoveAt(tlpMesh.RowStyles.Count - 1);
                }
            }

            tlpMesh.RowCount = _maze.Size.Height;

            ResetCellsToDefaultColors();
        }
        finally
        {
            Controls.Add(tlpMesh);
            tlpMesh.ResumeDrawing(callUpdate: false).ResumeLayout(performLayout: true);
        }
    }
    //0 using suspenddrawing is necessary to speed up the drawing process 

    public void ResetCellsToDefaultColors()
    {
        if (_maze == null) return;

        try
        {
            tlpMesh.SuspendDrawing();
            for (var row = 0; row < _maze.Size.Height; row++)
            {
                for (var column = 0; column < _maze.Size.Width; column++)
                {
                    var hittest = _maze.HitTest(new Point(column, row));

                    var preexistingLabel = tlpMesh.GetControlFromPosition(column, row);
                    if (hittest == MazeHitTestEnum.Free)
                    {
                        if (preexistingLabel != null) tlpMesh.Controls.Remove(preexistingLabel);
                        continue;
                    }

                    var properColorAndText = HitTestResultToColorAndText[hittest];
                    if (preexistingLabel == null) //0
                    {
                        tlpMesh.Controls.Add(SpawnControlForCell(properColorAndText.BackColor, properColorAndText.Font, properColorAndText.FontColor, properColorAndText.Tag), column, row);
                        continue;
                    }

                    preexistingLabel.Text = properColorAndText.Tag;
                    preexistingLabel.Font = properColorAndText.Font;
                    preexistingLabel.BackColor = properColorAndText.BackColor;
                    preexistingLabel.ForeColor = properColorAndText.FontColor;
                }
            }
        }
        finally
        {
            tlpMesh.ResumeDrawing();
        }

        //0 this method is also being used by setupmaze thus it needs to tread carefully when a cell has not filled yet with a control
    }

    static private Label SpawnControlForCell(Color backcolor, Font font, Color fontcolor, string text) => new()
    {
        Text = text,
        Font = font,
        Name = "dummyLabel",
        Dock = DockStyle.Fill,
        Size = new Size(20, 20),
        Margin = new Padding(0),
        TabIndex = 0,
        AutoSize = true,
        Location = new Point(1, 1),
        BackColor = backcolor,
        ForeColor = fontcolor,
        TextAlign = ContentAlignment.MiddleCenter
    };

    static private readonly Color White = Color.White;
    static private readonly Font FontForExitCell = new("Tahoma", 32F, FontStyle.Regular, GraphicsUnit.Point, 0);
    static private readonly Font FontForStartCell = new("Tahoma", 21F, FontStyle.Regular, GraphicsUnit.Point, 0);
    static private readonly Font FontForSimpleCells = new("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
    static private readonly Font FontForRoadblockCells = new("Tahoma", 42F, FontStyle.Regular, GraphicsUnit.Point, 0);

    static public readonly ImmutableDictionary<MazeHitTestEnum, (Color BackColor, Font Font, Color FontColor, string Tag)> HitTestResultToColorAndText = new[]
        { //@formatter:off
            //{MazeHitTestEnum.Free, new Tuple<Color, Font, string>(White, FontForSimpleCells, "")}, //not needed
            (HitTestResult: MazeHitTestEnum.Roadblock,  BackColor: Color.LightGray,       Font: FontForRoadblockCells,  FontColor: Color.DimGray,      Tag: RoadBlockTag ),
            (HitTestResult: MazeHitTestEnum.Exitpoint,  BackColor: Color.DarkGoldenrod,   Font: FontForExitCell,        FontColor: Color.Gold,         Tag: ExitTag      ),
            (HitTestResult: MazeHitTestEnum.Entrypoint, BackColor: Color.LightSeaGreen,   Font: FontForStartCell,       FontColor: Color.FloralWhite,  Tag: StartTag     ),
        } //@formatter:on
        .Select(x => new KeyValuePair<MazeHitTestEnum, (Color BackColor, Font Font, Color FontColor, string Tag)>(x.HitTestResult, (x.BackColor, x.Font, x.FontColor, x.Tag)))
        .ToImmutableDictionary();

    private const string ExitTag = "🥇";
    private const string StartTag = "🏠";
    private const string RoadBlockTag = "🪨";
}