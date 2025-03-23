using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazeRunner.Contracts.Events;

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

    public override string ToString() => ToStringy(includeShortestPath: true);
    
    public string ToStringy(bool includeShortestPath)
    {
        return $"""
                Engine: {Engine.GetEngineName()}
                Number of laps: {Repetitions} (smooth laps: {Repetitions - Crashes}, crashes: {Crashes})
                Path-lengths (Best / Worst / Average): {BestPathLength} / {WorstPathLength} / {AveragePathLength:N2}
                Time-durations (Best / Worst / Average): {BestTimePerformance.TotalMilliseconds}ms / {WorstTimePerformance.TotalMilliseconds}ms / {AverageTimePerformance.TotalMilliseconds:N2}ms
                {(includeShortestPath
                    ? $"""
                       Best (shortest) Path Found (coordinates are one-based, not zero-based):
                       {string.Join(" -> ", ShortestPath.Select(p => $"({p.X + 1},{p.Y + 1})"))}
                       """
                    : "")}
                """;
    }
}
