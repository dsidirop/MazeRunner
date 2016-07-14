using System;

namespace MazeRunner.Shared.Interfaces
{
    public class SingleEngineTestsStartingEventArgs : EventArgs
    {
        public IMazeRunnerEngine Engine;
    }

    public class SingleEngineTestsCompletedEventArgs : EventArgs
    {
        public IMazeRunnerEngine Engine;

        public int Crashes;
        public int Repetitions;

        public int BestPathLength;
        public int WorstPathLength;
        public double AveragePathLength;

        public TimeSpan BestTimePerformance;
        public TimeSpan WorstTimePerformance;
        public TimeSpan AverageTimePerformance;
    }
}