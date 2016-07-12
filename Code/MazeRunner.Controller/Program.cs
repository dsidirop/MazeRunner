using System;
using MazeRunner.Controller.Engine;
using MazeRunner.EnginesFactory;
using MazeRunner.Mazes;

namespace MazeRunner.Controller
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            Environment.ExitCode = new ControllerEngine(EnginesFactorySingleton.I, MazesFactorySingleton.I, Console.Out, Console.Error).Run(args);
        }
    }
}
