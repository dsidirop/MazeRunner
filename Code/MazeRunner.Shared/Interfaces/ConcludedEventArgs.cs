using System;

namespace MazeRunner.Shared.Interfaces
{
    public class ConcludedEventArgs : EventArgs
    {
        public bool Success;
        public bool Crashed;
    }
}