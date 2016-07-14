using System;
using System.IO;
using System.Linq;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Controller.Engine
{
    public partial class ControllerEngine
    {
        internal int? TryRunEngine(string[] args)
        {
            if ((args.Length < 2 && args.Length > 4)
                || args.FindParameter("engines=") == null
                || args.FindParameter("mazefile=") == null
                || (args.Length == 3 && args.FindParameter("repeat=") == null && args.FindParameter("verbose") == null)
                || (args.Length == 4 && (args.FindParameter("repeat=") == null || args.FindParameter("verbose") == null))) return null;

            var exitcode = 0;
            try
            {
                var verbose = args.FindParameter("verbose") != null;
                var mazefile = args.FindParameter("mazefile=").TryGetParameterValueString();
                var repetitions = Math.Max(1, args.FindParameter("repeat=").TryGetParameterValueInt()); //optional
                var enginenames = args.FindParameter("engines=").TryGetParameterValueString().ParseEngineNames(_enginesFactory);

                var maze = _mazesFactory.FromFile(mazefile, suppressExceptions: false);
                var enginesToBenchmark = enginenames.Select(x => _enginesFactory.Spawn(x, maze)).ToList();

                _enginesBenchmarker.LapCompleted += (s, ea) => //per lap
                {
                    if (!verbose) return;

                    var solution = maze.ToAsciiMap(p => ea.Engine.Trajectory.Contains(p) ? '*' : (ea.Engine.InvalidatedSquares.Contains(p) ? (char?)'#' : null));
                    _standardOutput.WriteLine(
                        $"RUN#{ea.LapIndex + 1} -> {(ea.Engine.TrajectoryTip == null ? "FAILURE" : $"SUCCESS(pathlength={ea.Engine.TrajectoryLength},timespan={ea.Duration.TotalMilliseconds}) -> {string.Join(" -> ", ea.Engine.Trajectory.Select(p => $"({p.X},{p.Y})"))}{nl}")}{nl}" +
                        $"{solution}{nl2}" +
                        $"X=wall, *=trajectory, #=visited{nl}");
                };
                _enginesBenchmarker.SingleEngineTestsCompleted += (s, ea) => //final
                {
                    _standardOutput.WriteLine(
                        $"{nl}" +
                        $"Engine: {ea.Engine.GetType().Name}{nl}" +
                        $"Number of runs: {ea.Repetitions} (smooth runs: {ea.Repetitions - ea.Crashes}, crashes: {ea.Crashes}){nl}" +
                        $"Path-lengths (Best / Worst / Average): {ea.BestPathLength} / {ea.WorstPathLength} / {ea.AveragePathLength}{nl}" +
                        $"Time-durations (Best / Worst / Average): {ea.BestTimePerformance.TotalMilliseconds}ms / {ea.WorstTimePerformance.TotalMilliseconds}ms / {ea.AverageTimePerformance.TotalMilliseconds}ms{nl}");
                };

                _enginesBenchmarker.Run(enginesToBenchmark, repetitions);
            }
            catch (Exception ex)
            {
                exitcode = ex is InvalidCommandLineArgumentException ? 1 : (ex is InvalidDataException ? 2 : 3); //mazefactory vs engine error
                _standardError.WriteLine($@"Failed to run. Reason: {ex.Message}");
            }

            return exitcode;
        }
    }
}