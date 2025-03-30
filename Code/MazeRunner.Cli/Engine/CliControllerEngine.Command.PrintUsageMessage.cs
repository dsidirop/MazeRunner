using System;
using MazeRunner.Cli.Enums;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    internal EExitCodes PrintUsageMessage(string[] args)
    {
        var exitcode = args == null || args.Length == 0 || args.Length == 1 && args.FindParameter("help") != null
            ? EExitCodes.Success
            : EExitCodes.InvalidCommandLineArguments;

        const string programName = "MazeRunner.Cli";
        _standardOutput.WriteLine(
            $"Usage:{nl2}" +
            $"{programName} --help{nl}" +
            $"{programName} --listengines{nl}" +
            $"{programName} --engines=[enginename1,enginename2|all]   --mazefile=path  [--repeat=number]  [--verbose]{nl}" +
            $"{programName} --generatemaze  --width=width  --height=height  --walldensity=walldensity  --output=path{nl}");

        return exitcode;
    }

    static private readonly string nl = Environment.NewLine;
    static private readonly string nl2 = nl + nl;
}
