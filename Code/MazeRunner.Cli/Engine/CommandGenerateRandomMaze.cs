using System;
using System.IO;
using MazeRunner.Utils;

namespace MazeRunner.Cli.Engine;

public partial class ControllerEngine
{
    internal int? TryGenerateRandomMaze(string[] args)
    {
        if (args.Length != 5
            || args.FindParameter("width=") == null
            || args.FindParameter("output=") == null
            || args.FindParameter("height=") == null
            || args.FindParameter("walldensity=") == null
            || args.FindParameter("generatemaze") == null)
            return null;

        var exitcode = 0;
        try
        {
            var width = args.FindParameter("width=").TryGetParameterValueInt();
            var height = args.FindParameter("height=").TryGetParameterValueInt();
            var outputfile = args.FindParameter("output=").TryGetParameterValueString();
            var walldensity = args.FindParameter("walldensity=").TryGetParameterValueDouble();

            File.WriteAllText(outputfile, _mazesFactory.Random(width, height, walldensity).ToAsciiMap());
        }
        catch (Exception ex)
        {
            exitcode = ex is InvalidCommandLineArgumentException ? 1 : 2;
            _standardError.WriteLine($@"Failed to generate random maze. Reason: {ex.Message}");
        }

        return exitcode;
    }
}