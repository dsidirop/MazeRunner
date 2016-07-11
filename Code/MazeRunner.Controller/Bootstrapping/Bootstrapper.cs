using MazeRunner.Controller.Bootstrapping.Commands;

namespace MazeRunner.Controller.Bootstrapping
{
    static public class Bootstrapper
    {
        static public int Run(string[] args)
        {
            var exitcode = 0;
            if (args.Length == 1 && args.FindParameter("listengines") != null)
            {
                exitcode = Command.ListEngines();
            }
            else if ((args.Length == 2 || args.Length == 3) && args.FindParameter("engine=") != null && args.FindParameter("mazefile=") != null && (args.Length == 2 || args.FindParameter("repeat=") != null))
            {
                exitcode = Command.RunEngine(args);
            }
            else if (args.Length == 5 && args.FindParameter("generatemaze") != null && args.FindParameter("width=") != null && args.FindParameter("height=") != null && args.FindParameter("walldensity=") != null && args.FindParameter("output=") != null)
            {
                exitcode = Command.GenerateRandomMaze(args);
            }
            else //--help
            {
                exitcode = Command.PrintUsageMessage(args);
            }
            return exitcode;
        }
    }
}
