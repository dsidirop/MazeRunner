using System;
using System.Collections.Generic;

namespace MazeRunner.Contracts.Events;

[Serializable]
public class CommencingEventArgs : EventArgs
{
    public int BenchmarkId;

    public int RepetitionsPerEngine;
    public IReadOnlyCollection<IMazeRunnerEngine> Engines;
}