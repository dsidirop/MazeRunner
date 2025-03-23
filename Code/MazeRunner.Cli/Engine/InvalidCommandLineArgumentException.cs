using System;

namespace MazeRunner.Cli.Engine;

internal class InvalidCommandLineArgumentException : Exception
{
    internal InvalidCommandLineArgumentException(string message) : base(message)
    {
    }
}