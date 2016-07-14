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
        private const int CellEdgeLength = 20;

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

        public CCMazeCanvas SetCustomColorOnCell(Point cellCoords, Color color)
        {
            var control = tlpMesh.GetControlFromPosition(cellCoords.X, cellCoords.Y);
            if (control == null) throw new ArgumentOutOfRangeException(nameof(cellCoords));

            control.BackColor = color;
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

                for (var row = 0; row < maze.Size.Height; row++)
                {
                    for (var column = 0; column < maze.Size.Width; column++)
                    {
                        var properColorAndText = HitTestToColorAndText[_maze.HitTest(new Point(column, row))];
                        var preexistingControl = tlpMesh.GetControlFromPosition(column, row);
                        if (preexistingControl != null)
                        {
                            preexistingControl.Text = properColorAndText.Item2;
                            preexistingControl.BackColor = properColorAndText.Item1;
                        }
                        else
                        {
                            tlpMesh.Controls.Add(SpawnCellControl(properColorAndText.Item1, properColorAndText.Item2), column, row);
                        }
                    }
                }
            }
            finally
            {
                tlpMesh.ResumeLayout(performLayout: true);
            }
        }

        static private Label SpawnCellControl(Color color, string text)
        {
            return new Label
            {
                Text = text,
                Font = FontForCells,
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
        static private readonly Font FontForCells = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        static public readonly Dictionary<MazeHitTestEnum, Tuple<Color, string>> HitTestToColorAndText = new Dictionary<MazeHitTestEnum, Tuple<Color, string>>
        {
            {MazeHitTestEnum.Free, new Tuple<Color, string>(White, "")},
            {MazeHitTestEnum.Roadblock, new Tuple<Color, string>(Color.Black, "")},
            {MazeHitTestEnum.Exitpoint, new Tuple<Color, string>(Color.Green, "G")},
            {MazeHitTestEnum.Entrypoint, new Tuple<Color, string>(Color.Goldenrod, "S")}
        };
    }
}
