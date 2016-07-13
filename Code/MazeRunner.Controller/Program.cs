using System;
using MazeRunner.Controller.Engine;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Mazes;

namespace MazeRunner.Controller
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            Environment.ExitCode = new ControllerEngine(EnginesFactorySingleton.I, new MazesFactory(), new EnginesTestbench(), Console.Out, Console.Error).Run(args);
        }
    }
}
