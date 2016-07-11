using System;
using System.IO;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    static internal partial class Command
    {
        static internal void GenerateRandomMaze(string[] args)
        {
            var width = args.FindParameter("width=").TryGetParameterValueInt();
            var height = args.FindParameter("height=").TryGetParameterValueInt();
            var outputfile = args.FindParameter("output=").TryGetParameterValueString();
            var walldensity = args.FindParameter("walldensity=").TryGetParameterValueDouble();

            try
            {
                File.WriteAllText(outputfile, MazeFactorySingleton.I.Random(width, height, walldensity).ToAsciiMap());
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 2;
                Console.Error.WriteLine($@"Failed to generate random maze: {ex.Message}");
            }
        }
    }
}