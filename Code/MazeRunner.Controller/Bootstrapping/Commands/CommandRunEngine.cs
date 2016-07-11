using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MazeRunner.EnginesFactory;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    static internal partial class Command
    {
        static internal void RunEngine(string[] args)
        {
            var mazefile = args.FindParameter("mazefile=").TryGetParameterValueString();
            var enginename = args.FindParameter("engine=").TryGetParameterValueString();
            var repetitions = Math.Max(1, args.FindParameter("repeat=").TryGetParameterValueInt(isoptional: true));

            try
            {
                var maze = MazeFactorySingleton.I.FromFile(mazefile);
                var engine = EnginesFactorySingleton.I.Spawn(enginename, maze);

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
                    Console.Out.WriteLine(
                        $"{(engine.TrajectoryTip == null ? "FAILURE" : $"SUCCESS({engine.TrajectoryLength}){nl}{string.Join(" → ", engine.Trajectory.Select(p => $"({p.X},{p.Y})"))}{nl}")}{nl}" +
                        $"{solution}{nl}" +
                        $"X=wall, *=trajectory, #=visited{nl}");
                }

                pathlengths.Sort();
                timedurations.Sort();
                Console.Out.WriteLine(
                    $"Path-lengths (Best / Worst / Average): {pathlengths.First()} / {pathlengths.Last()} / {pathlengths.Average()}{nl}" +
                    $"Time-duration in ms (Best / Worst / Average): {timedurations.First().TotalMilliseconds} / {timedurations.First().TotalMilliseconds} / {new TimeSpan(Convert.ToInt64(timedurations.Average(timeSpan => timeSpan.Ticks)))}{nl}");
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 2;
                Console.Error.WriteLine($@"Failed to run engine '{enginename}' ({ex.Message})");
            }
        }
    }
}