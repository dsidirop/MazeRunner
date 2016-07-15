using System;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class LapConcludedEventArgs : EventArgs
    {
        public int BenchmarkId;

        public int LapIndex;
        public TimeSpan Duration;
        public IMazeRunnerEngine Engine;
        public ConclusionStatusTypeEnum Status;
    }
}