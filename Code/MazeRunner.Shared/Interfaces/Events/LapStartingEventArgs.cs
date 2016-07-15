using System;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class LapStartingEventArgs : EventArgs
    {
        public int BenchmarkId;

        public int LapIndex;
        public IMazeRunnerEngine Engine;
    }
}