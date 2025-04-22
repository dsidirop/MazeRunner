using System;
using System.Collections.Generic;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct BenchmarkingCommencingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;
    public readonly int LapsPerEngine;
    public readonly IReadOnlyCollection<IMazeRunnerEngine> Engines;

    public BenchmarkingCommencingEventArgs(int benchmarkId, IReadOnlyCollection<IMazeRunnerEngine> engines, int lapsPerEngine)
    {
        Engines = engines;
        BenchmarkId = benchmarkId;
        LapsPerEngine = lapsPerEngine;
    }
    
    [Obsolete("This constructor should not be used")]
    public BenchmarkingCommencingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
