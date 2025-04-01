using System;
using System.Drawing;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct StateChangedEventArgs : IMazeRunnerEventArgs
{
    public readonly Point? OldTip; //can be null at the very beginning
    public readonly Point? NewTip; //can be null if we backtrack all the way past the start
    public readonly int StepIndex;
    public readonly bool IsProgressNotBacktracking;

    public StateChangedEventArgs(Point? oldTip, Point? newTip, int stepIndex, bool isProgressNotBacktracking)
    {
        OldTip = oldTip;
        NewTip = newTip;
        StepIndex = stepIndex;
        IsProgressNotBacktracking = isProgressNotBacktracking;
    }
    
    [Obsolete("This constructor should not be used")]
    public StateChangedEventArgs() => throw new NotImplementedException("This constructor should not be used");

    public override string ToString() => $"Step#{StepIndex}->{(IsProgressNotBacktracking ? "Progressed" : "Backtracked")} OldTip {OldTip?.ToString() ?? "N/A"} -> NewTip {NewTip?.ToString() ?? "N/A"}";
}
