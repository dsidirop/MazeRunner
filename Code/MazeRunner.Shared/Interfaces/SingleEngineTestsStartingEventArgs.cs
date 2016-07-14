using System;

namespace MazeRunner.Shared.Interfaces
{
    [Serializable]
    public class SingleEngineTestsStartingEventArgs : EventArgs
    {
        public IMazeRunnerEngine Engine;
    }
}