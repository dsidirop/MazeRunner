using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineSingleLapConcludedEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly int LapIndex;
    public readonly TimeSpan Duration;
    public readonly IMazeRunnerEngine Engine;
    public readonly ConclusionStatusTypeEnum Status;

    public SpecificEngineSingleLapConcludedEventArgs(int benchmarkId, int lapIndex, TimeSpan duration, IMazeRunnerEngine engine, ConclusionStatusTypeEnum status)
    {
        Status = status;
        Engine = engine;
        LapIndex = lapIndex;
        Duration = duration;
        BenchmarkId = benchmarkId;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineSingleLapConcludedEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
