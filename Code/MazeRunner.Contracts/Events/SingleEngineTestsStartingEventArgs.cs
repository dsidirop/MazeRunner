using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct SingleEngineTestsStartingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly IMazeRunnerEngine Engine;

    public SingleEngineTestsStartingEventArgs(int benchmarkId, IMazeRunnerEngine engine)
    {
        Engine = engine;
        BenchmarkId = benchmarkId;
    }
    
    [Obsolete("This constructor should not be used")]
    public SingleEngineTestsStartingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}