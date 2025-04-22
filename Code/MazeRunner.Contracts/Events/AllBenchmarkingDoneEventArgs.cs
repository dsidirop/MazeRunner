using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct AllBenchmarkingDoneEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public AllBenchmarkingDoneEventArgs(int benchmarkId)
    {
        BenchmarkId = benchmarkId;
    }

    [Obsolete("This constructor should not be used")]
    public AllBenchmarkingDoneEventArgs() => throw new NotImplementedException("This constructor should not be used");
}