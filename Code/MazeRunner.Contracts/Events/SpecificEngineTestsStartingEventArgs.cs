using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineTestsStartingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly IMazeRunnerEngine Engine;

    public SpecificEngineTestsStartingEventArgs(int benchmarkId, IMazeRunnerEngine engine)
    {
        Engine = engine;
        BenchmarkId = benchmarkId;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineTestsStartingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}