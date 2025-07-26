using System;
using MazeRunner.Contracts;
using MazeRunner.Contracts.Events;

namespace MazeRunner.EnginesFactory.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineTestsSuiteStartingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly IMazeRunnerEngine Engine;

    public SpecificEngineTestsSuiteStartingEventArgs(int benchmarkId, IMazeRunnerEngine engine)
    {
        Engine = engine;
        BenchmarkId = benchmarkId;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineTestsSuiteStartingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}