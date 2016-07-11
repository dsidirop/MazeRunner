using MazeRunner.Controller.Bootstrapping.Commands;

namespace MazeRunner.Controller.Bootstrapping
{
    internal sealed class Bootstrapper
    {
        internal void Run(string[] args)
        {
            if (args.Length == 1 && args.FindParameter("listengines") != null)
            {
                Command.ListEngines();
            }
            else if ((args.Length == 2 || args.Length == 3) && args.FindParameter("engine=") != null && args.FindParameter("mazefile=") != null && (args.Length == 2 || args.FindParameter("repeat=") != null))
            {
                Command.RunEngine(args);
            }
            else if (args.Length == 5 && args.FindParameter("generatemaze") != null && args.FindParameter("width=") != null && args.FindParameter("height=") != null && args.FindParameter("walldensity=") != null && args.FindParameter("output=") != null)
            {
                Command.GenerateRandomMaze(args);
            }
            else //--help
            {
                Command.PrintUsageMessage();
            }
        }
    }
}
