using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Engine.Exceptions;
using MazeRunner.Cli.Enums;
using MazeRunner.Contracts.Events;
using MazeRunner.Utils;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    internal async Task<EExitCodes?> TryRunEngineAsync(string[] args, CancellationToken? cancellationToken = null)
    {
        var ct = cancellationToken ?? CancellationToken.None;

        if (args == null
            || args.Length is < 2 or > 4
            || args.FindParameter("engines=") == null
            || args.FindParameter("mazefile=") == null
            || (args.Length == 3 && args.FindParameter("repeat=") == null && args.FindParameter("verbose") == null)
            || (args.Length == 4 && (args.FindParameter("repeat=") == null || args.FindParameter("verbose") == null)))
            return null;

        var exitcode = EExitCodes.Success;
        try
        {
            var verbose = args.FindParameter("verbose") != null;
            var mazefile = args.FindParameter("mazefile=").TryGetParameterValueString();
            var repetitions = Math.Max(1, args.FindParameter("repeat=").TryGetParameterValueInt()); //optional
            var enginenames = args.FindParameter("engines=").TryGetParameterValueString().ParseEngineNames(_enginesFactory);

            var maze = await _mazesFactory.FromFileAsync(mazefile, suppressExceptions: false);
            var enginesToBenchmark = enginenames.Select(x => _enginesFactory.Spawn(x, maze)).ToArray();

            ct.ThrowIfCancellationRequested();

            try
            {
                _enginesTestbench.SpecificEngineLapConcluded += EnginesTestbench_SpecificEngineLapConcluded_;
                _enginesTestbench.SpecificEngineTestsCompleted += EnginesTestbench_SpecificEngineTestsCompleted_;
                await _enginesTestbench.RunAsync(enginesToBenchmark, repetitions, ct);
            }
            finally
            {
                _enginesTestbench.SpecificEngineLapConcluded -= EnginesTestbench_SpecificEngineLapConcluded_;
                _enginesTestbench.SpecificEngineTestsCompleted -= EnginesTestbench_SpecificEngineTestsCompleted_;
            }

            void EnginesTestbench_SpecificEngineLapConcluded_(object _, SpecificEngineLapConcludedEventArgs ea)
            {
                if (!verbose) return;

                var solution = maze.ToAsciiMap(p => ea.Engine.Trajectory.Contains(p)
                    ? '*'
                    : (ea.Engine.InvalidatedSquares.Contains(p)
                        ? '#'
                        : null));

                _standardOutput.WriteLine(
                    $"""
                     RUN#{ea.LapIndex + 1} -> {(
                         ea.Engine.TrajectoryTip == null
                             ? "FAILURE"
                             : $"SUCCESS(pathlength={ea.Engine.TrajectoryLength},timespan={ea.Duration.TotalMilliseconds}) -> {ea.Engine.Trajectory.Select(p => $"({p.X},{p.Y})").Joinify(" -> ")}\n"
                     )}

                     {solution}

                     X=wall, *=trajectory, #=visited

                     """
                );
            }

            void EnginesTestbench_SpecificEngineTestsCompleted_(object _, SpecificEngineTestsCompletedEventArgs ea)
            {
                _standardOutput.WriteLine(
                    $"""

                     Engine: {ea.Engine.GetEngineName()}
                     Number of runs: {ea.Repetitions} (smooth runs: {ea.Repetitions - ea.Crashes}, crashes: {ea.Crashes})
                     Path-lengths (Best / Worst / Average): {ea.BestPathLength} / {ea.WorstPathLength} / {ea.AveragePathLength}
                     Time-durations (Best / Worst / Average): {ea.BestTimePerformance.TotalMilliseconds}ms / {ea.WorstTimePerformance.TotalMilliseconds}ms / {ea.AverageTimePerformance.TotalMilliseconds}ms

                     """
                );
            }
        }
        catch (Exception ex)
        {
            exitcode = GetProperExitCodeBasedOnException_(ex);

            if (ex is OperationCanceledException)
            {
                await _standardOutput.WriteLineAsync("Benchmarking run cancelled ...");
            }
            else
            {
                await _standardError.WriteLineAsync($"Failed to run. Reason: {ex.Message}");
            }
        }

        return exitcode;
        
        static EExitCodes GetProperExitCodeBasedOnException_(Exception ex)
        {
            return ex switch //mazefactory vs engine error
            {
                InvalidDataException => EExitCodes.InvalidMazeFile,
                OperationCanceledException => EExitCodes.Cancelled,
                FileNotFoundException
                    or ArgumentException //invalid maze dimensions
                    or ArgumentOutOfRangeException //engine name doesnt correspond to any engine
                    or InvalidCommandLineArgumentException => EExitCodes.InvalidCommandLineArguments,
                _ => EExitCodes.InternalError
            };
        }
    }
}