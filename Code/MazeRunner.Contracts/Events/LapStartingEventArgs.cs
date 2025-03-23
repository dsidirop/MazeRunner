using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public class LapStartingEventArgs : EventArgs
{
    public int BenchmarkId;

    public int LapIndex;
    public IMazeRunnerEngine Engine;
}