using System.Windows.Forms;
using System.ComponentModel;

namespace MazeRunner.TestbedUI.Helpers;

public sealed partial class FormGenerateNewRandomMaze : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MazeWidth
    {
        get => (int) nudWidth.Value;
        set => nudWidth.Value = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MazeHeight
    {
        get => (int) nudHeight.Value;
        set => nudHeight.Value = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double MazeDensity
    {
        get => (double) nudDensity.Value;
        set => nudDensity.Value = (decimal) value;
    }

    public FormGenerateNewRandomMaze()
    {
        InitializeComponent();
    }
}