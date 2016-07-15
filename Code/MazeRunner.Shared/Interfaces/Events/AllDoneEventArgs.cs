using System;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class AllDoneEventArgs : EventArgs
    {
        public int BenchmarkId;
    }
}