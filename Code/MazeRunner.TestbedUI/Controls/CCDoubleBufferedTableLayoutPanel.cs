using System.Windows.Forms;

namespace MazeRunner.TestbedUI.Controls
{
    public sealed class CCDoubleBufferedTableLayoutPanel : TableLayoutPanel
    {
        public CCDoubleBufferedTableLayoutPanel()
        {
            DoubleBuffered = true;
        }
    }
}