using System;
using System.Collections.Generic;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct CommencingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;
    public readonly int RepetitionsPerEngine;
    public readonly IReadOnlyCollection<IMazeRunnerEngine> Engines;

    public CommencingEventArgs(int benchmarkId, IReadOnlyCollection<IMazeRunnerEngine> engines, int repetitionsPerEngine)
    {
        Engines = engines;
        BenchmarkId = benchmarkId;
        RepetitionsPerEngine = repetitionsPerEngine;
    }
    
    [Obsolete("This constructor should not be used")]
    public CommencingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
