using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Enums;
using MazeRunner.Contracts;
using MazeRunner.Mazes;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    private readonly TextWriter _standardError;
    private readonly TextWriter _standardOutput;
    private readonly IMazesFactory _mazesFactory;
    private readonly IEnginesFactory _enginesFactory;
    private readonly IEnginesTestbench _enginesTestbench;

    public CliControllerEngine(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesTestbench, TextWriter standardOutput, TextWriter standardError)
    {
        _mazesFactory = mazesFactory;
        _standardError = standardError;
        _standardOutput = standardOutput;
        _enginesFactory = enginesFactory;
        _enginesTestbench = enginesTestbench;
    }

    public async Task<EExitCodes> RunAsync(string[] args, CancellationToken? cancellationToken = null)
    {
        var exitcode = (EExitCodes?) EExitCodes.Success;
        if ((exitcode = await TryRunEngineAsync(args, cancellationToken)) != null) return exitcode.Value;
        if ((exitcode = await TryListEnginesAsync(args, cancellationToken)) != null) return exitcode.Value;
        if ((exitcode = await TryGenerateRandomMazeAsync(args, cancellationToken)) != null) return exitcode.Value;

        return PrintUsageMessage(args);
    }
}
