using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MazeRunner.Shared;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
using System.Collections.Generic;

namespace MazeRunner.TestbedUI.CompositeControls
{
    public partial class CCMazeCanvas : UserControl
    {
        private const int CellEdgeLength = 50;

        private RowStyle StandardRowStyle => new RowStyle(SizeType.Absolute, height: CellEdgeLength);
        private ColumnStyle StandardColumnStyle => new ColumnStyle(SizeType.Absolute, width: CellEdgeLength);

        private IMaze _maze;
        public IMaze Maze
        {
            set
            {
                if (_maze == value) return;

                SetMaze(_maze = value);
            }
            get { return _maze; }
        }

        public CCMazeCanvas()
        {
            InitializeComponent();

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

        public CCMazeCanvas CustomizeCell(Point cellCoords, Color color, string textToAppend = null)
        {
            var label = tlpMesh.GetControlFromPosition(cellCoords.X, cellCoords.Y);
            if (label == null) throw new ArgumentOutOfRangeException(nameof(cellCoords));

            label.BackColor = color;
            if (!string.IsNullOrEmpty(textToAppend))
            {
                label.Text += $"{(string.IsNullOrEmpty(label.Text) ? "" : (label.Text == StartTag || label.Text == ExitTag ? ": " : ", "))}{textToAppend}";
            }

            return this;
        }
        //0 we need to force a redraw only of a specific cell in the tlp   thus we calculate its client rectangle and invoke invalidate on it followed by update

        private void SetMaze(IMaze maze)
        {
            try
            {
                tlpMesh.SuspendLayout();
                while (tlpMesh.ColumnStyles.Count != maze.Size.Width)
                {
                    if (tlpMesh.ColumnStyles.Count < maze.Size.Width)
                    {
                        tlpMesh.ColumnStyles.Add(StandardColumnStyle);
                    }
                    else //>
                    {
                        tlpMesh.ColumnStyles.RemoveAt(tlpMesh.ColumnStyles.Count - 1);
                    }
                }
                tlpMesh.ColumnCount = maze.Size.Width;

                while (tlpMesh.RowStyles.Count != maze.Size.Height)
                {
                    if (tlpMesh.RowStyles.Count < maze.Size.Height)
                    {
                        tlpMesh.RowStyles.Add(StandardRowStyle);
                    }
                    else //>
                    {
                        tlpMesh.RowStyles.RemoveAt(tlpMesh.RowStyles.Count - 1);
                    }
                }
                tlpMesh.RowCount = maze.Size.Height;

                ResetCellsToDefaultColors();
            }
            finally
            {
                tlpMesh.ResumeLayout(performLayout: true);
            }
        }

        public void ResetCellsToDefaultColors()
        {
            if (_maze == null) return;

            try
            {
                tlpMesh.SuspendLayout();
                for (var row = 0; row < _maze.Size.Height; row++)
                {
                    for (var column = 0; column < _maze.Size.Width; column++)
                    {
                        var properColorAndText = HitTestToColorAndText[_maze.HitTest(new Point(column, row))];
                        var preexistingControl = tlpMesh.GetControlFromPosition(column, row);
                        if (preexistingControl != null)
                        {
                            preexistingControl.Text = properColorAndText.Item3;
                            preexistingControl.Font = properColorAndText.Item2;
                            preexistingControl.BackColor = properColorAndText.Item1;
                        }
                        else
                        {
                            tlpMesh.Controls.Add(SpawnCellControl(properColorAndText.Item1, properColorAndText.Item2, properColorAndText.Item3), column, row);
                        }
                    }
                }
            }
            finally
            {
                tlpMesh.ResumeLayout(performLayout: true);
            }
        }

        static private Label SpawnCellControl(Color color, Font font, string text)
        {
            return new Label
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
                BackColor = color,
                ForeColor = White,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        static private readonly Color White = Color.White;
        static private readonly Font FontForSimpleCells = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        static private readonly Font FontForStartAndEndCells = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        static public readonly Dictionary<MazeHitTestEnum, Tuple<Color, Font, string>> HitTestToColorAndText = new Dictionary<MazeHitTestEnum, Tuple<Color, Font, string>>
        {
            {MazeHitTestEnum.Free, new Tuple<Color, Font, string>(White, FontForSimpleCells, "")},
            {MazeHitTestEnum.Roadblock, new Tuple<Color, Font, string>(Color.Black, FontForSimpleCells, "")},
            {MazeHitTestEnum.Exitpoint, new Tuple<Color, Font, string>(Color.Green, FontForStartAndEndCells, ExitTag)},
            {MazeHitTestEnum.Entrypoint, new Tuple<Color, Font, string>(Color.Goldenrod, FontForStartAndEndCells, StartTag)}
        };

        private const string ExitTag = "G";
        private const string StartTag = "S";
    }
}
