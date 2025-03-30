using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Enums;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    public async Task<EExitCodes> ProcessCliArgsAsync(string[] args, CancellationToken? cancellationToken = null)
    {
        using var localCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            _masterCancellationTokenSource.Token,
            cancellationToken ?? CancellationToken.None
        );
        
        var exitcode = (EExitCodes?) EExitCodes.Success;
        if ((exitcode = await TryRunEngineAsync(args, localCancellationTokenSource.Token)) != null) return exitcode.Value;
        if ((exitcode = await TryListEnginesAsync(args, localCancellationTokenSource.Token)) != null) return exitcode.Value;
        if ((exitcode = await TryGenerateRandomMazeAsync(args, localCancellationTokenSource.Token)) != null) return exitcode.Value;

        return PrintUsageMessage(args);
    }
}
