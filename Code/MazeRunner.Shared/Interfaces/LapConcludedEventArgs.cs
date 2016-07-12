using System;

namespace MazeRunner.Shared.Interfaces
{
    public class LapConcludedEventArgs : EventArgs
    {
        public int LapIndex;
        public TimeSpan Duration;
        public IMazeRunnerEngine Engine;
    }
}