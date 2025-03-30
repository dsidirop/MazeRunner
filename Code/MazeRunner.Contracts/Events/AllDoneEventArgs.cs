using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public class AllDoneEventArgs : EventArgs
{
    public int BenchmarkId;
}