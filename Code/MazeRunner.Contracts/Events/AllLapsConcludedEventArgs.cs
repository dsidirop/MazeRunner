using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct AllLapsConcludedEventArgs : IMazeRunnerEventArgs
{
    public readonly bool ExitpointReached;
    public readonly ConclusionStatusTypeEnum Status;

    public AllLapsConcludedEventArgs(bool exitpointReached, ConclusionStatusTypeEnum status)
    {
        Status = status;
        ExitpointReached = exitpointReached;
    }
    
    [Obsolete("This constructor should not be used")]
    public AllLapsConcludedEventArgs() => throw new NotImplementedException("This constructor should not be used");

    public override string ToString() => $"ConclusionStatus={Status} -> ExitPointReached {(ExitpointReached ? "YES" : "NO")}";
}
