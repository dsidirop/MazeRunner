using System;
using MazeRunner.Cli.Enums;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    public bool Cancel()
    {
        if (HasCancellationBeenAlreadyRequestedOnce)
        {
            _standardOutput.WriteLine("Exiting immediately ...");
            Environment.Exit((int) EExitCodes.Cancelled); //this kills the process on the spot
            return false; //not reached
        }

        HasCancellationBeenAlreadyRequestedOnce = true;
        
        _standardOutput.WriteAsync("Cancelling ... ");
        _masterCancellationTokenSource.Cancel();
        return true;
    }
}
