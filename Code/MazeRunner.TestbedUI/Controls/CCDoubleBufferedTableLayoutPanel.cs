using System.Windows.Forms;

namespace MazeRunner.TestbedUI.Controls
{
    public sealed class CCDoubleBufferedTableLayoutPanel : TableLayoutPanel
    {
        public CCDoubleBufferedTableLayoutPanel()
        {
            DoubleBuffered = true; //helps alot with performance when it comes to refreshing ccmaze control
        }
    }
}