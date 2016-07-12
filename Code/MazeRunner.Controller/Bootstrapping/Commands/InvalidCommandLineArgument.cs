using System;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    internal class InvalidCommandLineArgument : Exception
    {
        internal InvalidCommandLineArgument(string message) : base(message)
        {
        }
    }
}