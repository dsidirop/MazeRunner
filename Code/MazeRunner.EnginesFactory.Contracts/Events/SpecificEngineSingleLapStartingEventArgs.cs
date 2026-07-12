using System;
using MazeRunner.Contracts.Events;
using MazeRunner.Engines.Contracts;

namespace MazeRunner.EnginesFactory.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineSingleLapStartingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly int LapIndex;
    public readonly IMazeRunnerEngine Engine;

    public SpecificEngineSingleLapStartingEventArgs(int benchmarkId, int lapIndex, IMazeRunnerEngine engine)
    {
        Engine = engine;
        LapIndex = lapIndex;
        BenchmarkId = benchmarkId;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineSingleLapStartingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
