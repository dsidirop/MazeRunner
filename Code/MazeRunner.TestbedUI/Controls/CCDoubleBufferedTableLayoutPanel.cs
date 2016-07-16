using System;
using System.Windows.Forms;

namespace MazeRunner.TestbedUI.Controls
{
    public sealed class CCDoubleBufferedTableLayoutPanel : TableLayoutPanel
    {
        public CCDoubleBufferedTableLayoutPanel()
        {
            DoubleBuffered = true; //helps alot with performance when it comes to refreshing ccmaze control
        }

        private int _controlDrawingSuspendCounter;
        public TableLayoutPanel SuspendDrawing()
        {
            if (_controlDrawingSuspendCounter == 0)
            {
                var msgSuspendUpdate = Message.Create(Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
                NativeWindow.FromHandle(Handle).DefWndProc(ref msgSuspendUpdate);
            }
            _controlDrawingSuspendCounter++; //order
            return this;
        }

        public TableLayoutPanel ResumeDrawing(bool callUpdate = true)
        {
            if (_controlDrawingSuspendCounter > 0)
            {
                _controlDrawingSuspendCounter--;
            }

            if (_controlDrawingSuspendCounter > 0) return this;

            var handle = Handle;
            var msgResumeUpdate = Message.Create(handle, WM_SETREDRAW, wparam: new IntPtr(1), lparam: IntPtr.Zero);
            NativeWindow.FromHandle(handle).DefWndProc(ref msgResumeUpdate);
            Invalidate(true);
            if (callUpdate) Update();
            return this;
        }
        private const int WM_SETREDRAW = 0xB; //11
    }
}