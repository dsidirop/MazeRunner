﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MazeRunner.EnginesFactory;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    static internal partial class Command
    {
        static internal int RunEngine(string[] args)
        {
            var exitcode = 0;
            try
            {
                var mazefile = args.FindParameter("mazefile=").TryGetParameterValueString();
                var enginename = args.FindParameter("engine=").TryGetParameterValueString();
                var repetitions = Math.Max(1, args.FindParameter("repeat=").TryGetParameterValueInt()); //optional

                var maze = MazeFactorySingleton.I.FromFile(mazefile, suppressExceptions: false);
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
                        $"{solution}{nl}{nl}" +
                        $"X=wall, *=trajectory, #=visited{nl}");
                }

                pathlengths.Sort();
                timedurations.Sort();
                Console.Out.WriteLine(
                    $"Number of runs: {repetitions}{nl}" +
                    $"Path-lengths (Best / Worst / Average): {pathlengths.First()} / {pathlengths.Last()} / {pathlengths.Average()}{nl}" +
                    $"Time-duration in ms (Best / Worst / Average): {timedurations.First().TotalMilliseconds} / {timedurations.First().TotalMilliseconds} / {new TimeSpan(Convert.ToInt64(timedurations.Average(timeSpan => timeSpan.Ticks))).TotalMilliseconds}{nl}");
            }
            catch (Exception ex)
            {
                exitcode = ex is InvalidCommandLineArgument ? 1 : (ex is InvalidDataException ? 2 : 3); //mazefactory vs engine error
                Console.Error.WriteLine($@"Failed to run engine: {ex.Message}");
            }

            return exitcode;
        }
    }
}