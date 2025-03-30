using System;

namespace MazeRunner.Cli.Engine.Exceptions;

internal class InvalidCommandLineArgumentException : Exception
{
    public InvalidCommandLineArgumentException()
    {
    }

    internal InvalidCommandLineArgumentException(string message) : base(message)
    {
    }

    public InvalidCommandLineArgumentException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
