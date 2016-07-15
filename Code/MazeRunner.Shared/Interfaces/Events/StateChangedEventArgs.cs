using System;
using System.Drawing;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class StateChangedEventArgs : EventArgs
    {
        public Point? OldTip; //can be null at the very beginning
        public Point? NewTip; //can be null if we backtrack all the way past the start
        public int StepIndex;
        public bool IsProgressNotBacktracking;

        public override string ToString() => $"Step#{StepIndex}->{(IsProgressNotBacktracking ? "Progressed" : "Backtracked")} OldTip {OldTip?.ToString() ?? "N/A"} -> NewTip {NewTip?.ToString() ?? "N/A"}";
    }
}