using System;

namespace MazeRunner.Shared.Interfaces
{
    [Serializable]
    public class LapConcludedEventArgs : EventArgs
    {
        public int LapIndex;
        public TimeSpan Duration;
        public IMazeRunnerEngine Engine;
        public ConclusionStatusTypeEnum Status;
    }
}