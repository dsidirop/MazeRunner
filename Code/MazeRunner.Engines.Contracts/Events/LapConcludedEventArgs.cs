using System;
using MazeRunner.Contracts.Events;

namespace MazeRunner.Engines.Contracts.Events;

[Serializable]
public readonly struct LapConcludedEventArgs : IMazeRunnerEventArgs
{
    public readonly bool ExitpointReached;
    public readonly ConclusionStatusTypeEnum Status;

    public LapConcludedEventArgs(bool exitpointReached, ConclusionStatusTypeEnum status)
    {
        Status = status;
        ExitpointReached = exitpointReached;
    }
    
    [Obsolete("This constructor should not be used")]
    public LapConcludedEventArgs() => throw new NotImplementedException("This constructor should not be used");

    public override string ToString() => $"ConclusionStatus={Status} -> ExitPointReached {(ExitpointReached ? "YES" : "NO")}";
}