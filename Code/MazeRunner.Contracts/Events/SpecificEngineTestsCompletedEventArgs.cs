using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineTestsCompletedEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly IReadOnlyCollection<Point> ShortestPath;

    public readonly IMazeRunnerEngine Engine;

    public readonly int Crashes;
    public readonly int Repetitions;

    public readonly int BestPathLength;
    public readonly int WorstPathLength;
    public readonly double AveragePathLength;

    public readonly TimeSpan BestTimePerformance;
    public readonly TimeSpan WorstTimePerformance;
    public readonly TimeSpan AverageTimePerformance;

    public SpecificEngineTestsCompletedEventArgs(
        int benchmarkId,
        IReadOnlyCollection<Point> shortestPath,
        IMazeRunnerEngine engine,
        int crashes,
        int repetitions,
        int bestPathLength,
        int worstPathLength,
        double averagePathLength,
        TimeSpan bestTimePerformance,
        TimeSpan worstTimePerformance,
        TimeSpan averageTimePerformance
    )
    {
        BenchmarkId = benchmarkId;
        ShortestPath = shortestPath;
        Engine = engine;
        Crashes = crashes;
        Repetitions = repetitions;
        BestPathLength = bestPathLength;
        WorstPathLength = worstPathLength;
        AveragePathLength = averagePathLength;
        BestTimePerformance = bestTimePerformance;
        WorstTimePerformance = worstTimePerformance;
        AverageTimePerformance = averageTimePerformance;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineTestsCompletedEventArgs() => throw new NotImplementedException("This constructor should not be used");

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
