using System;
using MazeRunner.Contracts.Events;

namespace MazeRunner.EnginesFactory.Contracts.Events;

[Serializable]
public readonly struct AllBenchmarkingsDoneEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public AllBenchmarkingsDoneEventArgs(int benchmarkId)
    {
        BenchmarkId = benchmarkId;
    }

    [Obsolete("This constructor should not be used")]
    public AllBenchmarkingsDoneEventArgs() => throw new NotImplementedException("This constructor should not be used");
}