using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public class SingleEngineTestsStartingEventArgs : EventArgs
{
    public int BenchmarkId;

    public IMazeRunnerEngine Engine;
}