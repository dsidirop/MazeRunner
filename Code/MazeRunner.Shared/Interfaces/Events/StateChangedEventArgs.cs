using System;
using System.Drawing;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class StateChangedEventArgs : EventArgs
    {
        public int BenchmarkId;

        public Point? OldTip;
        public Point? NewTip;
        public int StepIndex;
        public bool IsProgressNotBacktracking;
    }
}