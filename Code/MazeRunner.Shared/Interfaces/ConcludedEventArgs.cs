using System;

namespace MazeRunner.Shared.Interfaces
{
    [Serializable]
    public class ConcludedEventArgs : EventArgs
    {
        public bool Success;
        public bool Crashed;
    }
}