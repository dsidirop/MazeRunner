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
            if ((args.Length != 2 && args.Length != 3) || args.FindParameter("engine=") == null || args.FindParameter("mazefile=") == null || (args.Length != 2 && args.FindParameter("repeat=") == null)) return null;

            var exitcode = 0;
            try
            {
                var mazefile = args.FindParameter("mazefile=").TryGetParameterValueString();
                var enginename = args.FindParameter("engine=").TryGetParameterValueString();
                var repetitions = Math.Max(1, args.FindParameter("repeat=").TryGetParameterValueInt()); //optional

                var maze = _mazesFactory.FromFile(mazefile, suppressExceptions: false);
                var engine = _enginesFactory.Spawn(enginename, maze);

                _enginesBenchmarker.LapCompleted += (s, ea) => //per lap
                {
                    var solution = maze.ToAsciiMap(p => ea.Engine.Trajectory.Contains(p) ? '*' : (ea.Engine.InvalidatedSquares.Contains(p) ? (char?)'#' : null));
                    _standardOutput.WriteLine(
                        $"RUN#{ea.LapIndex + 1} -> {(ea.Engine.TrajectoryTip == null ? "FAILURE" : $"SUCCESS(pathlength={ea.Engine.TrajectoryLength},timespan={ea.Duration.TotalMilliseconds}) -> {string.Join(" -> ", ea.Engine.Trajectory.Select(p => $"({p.X},{p.Y})"))}{nl}")}{nl}" +
                        $"{solution}{nl}{nl}" +
                        $"X=wall, *=trajectory, #=visited{nl}");
                };
                _enginesBenchmarker.SingleEngineTestsCompleted += (s, ea) => //final
                {
                    _standardOutput.WriteLine(
                        $"Engine: {ea.Engine.GetType().Name}{nl}" +
                        $"Number of runs: {repetitions} (smooth runs: {repetitions - ea.Crashes}, crashes: {ea.Crashes}){nl}" +
                        $"Path-lengths (Best / Worst / Average): {ea.BestPathLength} / {ea.WorstPathLength} / {ea.AveragePathLength}{nl}" +
                        $"Time-duration (Best / Worst / Average): {ea.BestTimePerformance.TotalMilliseconds}ms / {ea.WorstTimePerformance.TotalMilliseconds}ms / {ea.AverageTimePerformance.TotalMilliseconds}ms{nl}");
                };

                _enginesBenchmarker.Run(new[] {engine}, repetitions);
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