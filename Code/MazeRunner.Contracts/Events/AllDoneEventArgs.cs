using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct AllDoneEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public AllDoneEventArgs(int benchmarkId)
    {
        BenchmarkId = benchmarkId;
    }

    [Obsolete("This constructor should not be used")]
    public AllDoneEventArgs() => throw new NotImplementedException("This constructor should not be used");
}