using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared.Interfaces.Events
{
    [Serializable]
    public class SingleEngineTestsCompletedEventArgs : EventArgs
    {
        public int BenchmarkId;

        public IList<Point> ShortestPath;

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