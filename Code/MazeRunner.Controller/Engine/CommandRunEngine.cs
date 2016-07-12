using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                var stopWatch = new Stopwatch();
                var pathlengths = new List<int>(repetitions);
                var timedurations = new List<TimeSpan>(repetitions);
                for (var i = 0; i < repetitions; i++, engine.Reset())
                {
                    stopWatch.Restart();
                    engine.Run();
                    stopWatch.Stop();
                    pathlengths.Add(engine.TrajectoryLength);
                    timedurations.Add(stopWatch.Elapsed);

                    var solution = maze.ToAsciiMap(p => engine.Trajectory.Contains(p) ? '*' : (engine.InvalidatedSquares.Contains(p) ? (char?)'#' : null));
                    _standardOutput.WriteLine(
                        $"RUN#{i + 1} → {(engine.TrajectoryTip == null ? "FAILURE" : $"SUCCESS({engine.TrajectoryLength}) → {string.Join(" → ", engine.Trajectory.Select(p => $"({p.X},{p.Y})"))}{nl}")}{nl}" +
                        $"{solution}{nl}{nl}" +
                        $"X=wall, *=trajectory, #=visited{nl}");
                }

                pathlengths.Sort();
                timedurations.Sort();
                _standardOutput.WriteLine(
                    $"Number of runs: {repetitions}{nl}" +
                    $"Path-lengths (Best / Worst / Average): {pathlengths.First()} / {pathlengths.Last()} / {pathlengths.Average()}{nl}" +
                    $"Time-duration in ms (Best / Worst / Average): {timedurations.First().TotalMilliseconds} / {timedurations.Last().TotalMilliseconds} / {new TimeSpan(Convert.ToInt64(timedurations.Average(timeSpan => timeSpan.Ticks))).TotalMilliseconds}{nl}");
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