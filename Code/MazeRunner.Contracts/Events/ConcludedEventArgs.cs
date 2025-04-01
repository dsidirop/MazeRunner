using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct ConcludedEventArgs : IMazeRunnerEventArgs
{
    public readonly bool ExitpointReached;
    public readonly ConclusionStatusTypeEnum Status;

    public ConcludedEventArgs(bool exitpointReached, ConclusionStatusTypeEnum status)
    {
        Status = status;
        ExitpointReached = exitpointReached;
    }
    
    [Obsolete("This constructor should not be used")]
    public ConcludedEventArgs() => throw new NotImplementedException("This constructor should not be used");

    public override string ToString() => $"ConclusionStatus={Status} -> ExitpointReached {(ExitpointReached ? "YES" : "NO")}";
}
