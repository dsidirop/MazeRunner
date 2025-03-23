using System;
using System.Threading.Tasks;
using MazeRunner.Cli.Engine;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Mazes;

namespace MazeRunner.Cli;

static internal class Program
{
    static public async Task Main(string[] args)
    {
        Environment.ExitCode = await new ControllerEngine(EnginesFactorySingleton.I, new MazesFactory(), new EnginesTestbench(), Console.Out, Console.Error).RunAsync(args);
    }
}