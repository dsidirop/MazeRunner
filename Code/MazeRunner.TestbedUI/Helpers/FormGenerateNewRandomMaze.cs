using System.Windows.Forms;

namespace MazeRunner.TestbedUI.Helpers
{
    public sealed partial class FormGenerateNewRandomMaze : Form
    {
        public int MazeWidth
        {
            get { return (int) nudWidth.Value; }
            set { nudWidth.Value = value; }
        }

        public int MazeHeight
        {
            get { return (int) nudHeight.Value; }
            set { nudHeight.Value = value; }
        }

        public double MazeDensity
        {
            get { return (double) nudDensity.Value; }
            set { nudDensity.Value = (decimal) value; }
        }

        public FormGenerateNewRandomMaze()
        {
            InitializeComponent();
        }
    }
}
