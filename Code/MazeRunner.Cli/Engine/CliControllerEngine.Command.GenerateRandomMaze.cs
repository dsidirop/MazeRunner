using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Engine.Exceptions;
using MazeRunner.Cli.Enums;
using MazeRunner.Utils;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    internal async Task<EExitCodes?> TryGenerateRandomMazeAsync(string[] args, CancellationToken? cancellationToken)
    {
        if (args.Length != 5
            || args.FindParameter("width=") == null
            || args.FindParameter("output=") == null
            || args.FindParameter("height=") == null
            || args.FindParameter("walldensity=") == null
            || args.FindParameter("generatemaze") == null)
            return null;
        
        var ct = cancellationToken ?? CancellationToken.None;

        var exitcode = EExitCodes.Success;
        try
        {
            var width = args.FindParameter("width=").TryGetParameterValueInt();
            var height = args.FindParameter("height=").TryGetParameterValueInt();
            var outputfile = args.FindParameter("output=").TryGetParameterValueString();
            var walldensity = args.FindParameter("walldensity=").TryGetParameterValueDouble();

            var asciiMazeString = _mazesFactory
                .Random(width, height, walldensity)
                .ToAsciiMap();
            
            await File.WriteAllTextAsync(outputfile, asciiMazeString, ct);
        }
        catch (OperationCanceledException)
        {
            exitcode = EExitCodes.Cancelled;

            await _standardOutput.WriteLineAsync("Maze generation cancelled");
        }
        catch (Exception ex)
        {
            exitcode = GetProperExitCodeBasedOnException_(ex);

            await _standardError.WriteLineAsync($"Failed to generate random maze. Reason: {ex.Message}");
        }

        return exitcode;

        static EExitCodes GetProperExitCodeBasedOnException_(Exception ex)
        {
            return ex switch //mazefactory vs engine error
            {
                InvalidDataException => EExitCodes.InvalidMazeFile,
                OperationCanceledException => EExitCodes.Cancelled,
                FileNotFoundException
                    or ArgumentException
                    or ArgumentOutOfRangeException //engine name doesnt correspond to any engine
                    or InvalidCommandLineArgumentException => EExitCodes.InvalidCommandLineArguments,
                _ => EExitCodes.InternalError
            };
        }
    }
}
