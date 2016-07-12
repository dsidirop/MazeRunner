﻿using System.Linq;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Controller.Engine
{
    public partial class ControllerEngine
    {
        internal int PrintUsageMessage(string[] args)
        {
            var exitcode = !args.Any() || args.Length == 1 && args.FindParameter("help") != null ? 0 : 1;

            const string programname = "MazeRunner.Controller";
            _standardOutput.WriteLine(
                $@"Usage:{nl2}" +
                $@"{programname} --help{nl}" +
                $@"{programname} --listengines{nl}" +
                $@"{programname} --engine=enginename   --mazefile=path  [--repeat=number]{nl}" +
                $@"{programname} --generatemaze  --width=width  --height=height  --walldensity=walldensity  --output=path{nl}");

            return exitcode;
        }
        static private readonly string nl = Utilities.nl;
        static private readonly string nl2 = nl + nl;
    }
}