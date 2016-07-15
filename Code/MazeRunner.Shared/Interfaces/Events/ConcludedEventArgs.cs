using System;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class ConcludedEventArgs : EventArgs
    {
        public int BenchmarkId;

        public bool Success;
        public ConclusionStatusTypeEnum Status;
    }
}