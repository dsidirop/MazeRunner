using System;
using System.Collections.Generic;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct CommencingEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;
    public readonly int LapsPerEngine;
    public readonly IReadOnlyCollection<IMazeRunnerEngine> Engines;

    public CommencingEventArgs(int benchmarkId, IReadOnlyCollection<IMazeRunnerEngine> engines, int lapsPerEngine)
    {
        Engines = engines;
        BenchmarkId = benchmarkId;
        LapsPerEngine = lapsPerEngine;
    }
    
    [Obsolete("This constructor should not be used")]
    public CommencingEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
