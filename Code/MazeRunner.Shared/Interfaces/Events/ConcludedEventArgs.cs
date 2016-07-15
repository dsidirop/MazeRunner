using System;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class ConcludedEventArgs : EventArgs
    {
        public bool ExitpointReached;
        public ConclusionStatusTypeEnum Status;

        public override string ToString() => $"ConclusionStatus={Status} -> ExitpointReached {(ExitpointReached ? "YES" : "NO")}";
    }
}