using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct LapStartingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly int LapIndex;
    public readonly IMazeRunnerEngine Engine;

    public LapStartingEventArgs(int benchmarkId, int lapIndex, IMazeRunnerEngine engine)
    {
        BenchmarkId = benchmarkId;
        LapIndex = lapIndex;
        Engine = engine;
    }
    
    [Obsolete("This constructor should not be used")]
    public LapStartingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
