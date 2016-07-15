using System;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class SingleEngineTestsStartingEventArgs : EventArgs
    {
        public int BenchmarkId;

        public IMazeRunnerEngine Engine;
    }
}