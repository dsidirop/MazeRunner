using System;
using System.Linq;
using MazeRunner.EnginesFactory;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    static internal partial class Command
    {
        static internal int ListEngines()
        {
            Console.Out.WriteLine($"Available Engines:{nl}{nl}{string.Join(nl, EnginesFactorySingleton.I.Engines.Select(x => x.Key))}{nl}");
            return 0;
        }
    }
}
