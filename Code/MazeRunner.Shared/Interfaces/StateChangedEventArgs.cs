using System;
using System.Drawing;

namespace MazeRunner.Shared.Interfaces
{
    [Serializable]
    public class StateChangedEventArgs : EventArgs
    {
        public Point? OldTip;
        public Point? NewTip;
        public int StepIndex;
        public bool IsProgressNotBacktracking;
    }
}