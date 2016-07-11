using System;
using System.Diagnostics;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    static internal partial class Command
    {
        static internal void PrintUsageMessage()
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            Console.Out.WriteLine(
                $@"Usage:{nl2}" +
                $@"{processName} --help{nl}" +
                $@"{processName} --listengines{nl}" +
                $@"{processName} --engine=enginename   --mazefile=path  [--repeat=number]{nl}" +
                $@"{processName} --generatemaze  --width=width  --height=height  --walldensity=walldensity  --output=path{nl}");
        }
        static private readonly string nl = Utilities.nl;
        static private readonly string nl2 = nl + nl;
    }
}