using System;
using MazeRunner.Controller.Bootstrapping;

namespace MazeRunner.Controller
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            Environment.ExitCode = Bootstrapper.Run(args);
        }
    }
}
