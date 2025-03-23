using System;

namespace MazeRunner.Controller.Engine;

internal class InvalidCommandLineArgumentException : Exception
{
    internal InvalidCommandLineArgumentException(string message) : base(message)
    {
    }
}