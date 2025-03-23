using System.Windows.Forms;
using System.ComponentModel;

namespace MazeRunner.TestbedUI.Helpers;

public sealed partial class FormGenerateNewRandomMaze : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MazeWidth
    {
        get { return (int) nudWidth.Value; }
        set { nudWidth.Value = value; }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MazeHeight
    {
        get { return (int) nudHeight.Value; }
        set { nudHeight.Value = value; }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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