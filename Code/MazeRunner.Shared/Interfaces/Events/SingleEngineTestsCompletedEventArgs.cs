using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MazeRunner.Shared.Helpers;

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

        public string ToString(bool includeShortestPath)
        {
            return $"[#{BenchmarkId}] All laps completed for engine '{Engine.GetEngineName()}':{U.nl2}" +
                   $"Engine: {Engine.GetEngineName()}{U.nl}" +
                   $"Number of laps: {Repetitions} (smooth laps: {Repetitions - Crashes}, crashes: {Crashes}){U.nl}" +
                   $"Path-lengths (Best / Worst / Average): {BestPathLength} / {WorstPathLength} / {AveragePathLength:N2}{U.nl}" +
                   $"Time-durations (Best / Worst / Average): {BestTimePerformance.TotalMilliseconds}ms / {WorstTimePerformance.TotalMilliseconds}ms / {AverageTimePerformance.TotalMilliseconds:N2}ms" +
                   (includeShortestPath ? $"{U.nl}Best (shortest) Path Found (coordinates are one-based, not zero-based):{U.nl2}{string.Join(" -> ", ShortestPath.Select(p => $"({p.X + 1},{p.Y + 1})"))}" : "");
        }

        public override string ToString() => ToString(includeShortestPath: true);
    }
}