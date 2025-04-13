using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineLapStartingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly int LapIndex;
    public readonly IMazeRunnerEngine Engine;

    public SpecificEngineLapStartingEventArgs(int benchmarkId, int lapIndex, IMazeRunnerEngine engine)
    {
        BenchmarkId = benchmarkId;
        LapIndex = lapIndex;
        Engine = engine;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineLapStartingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
