using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Enums;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    internal async Task<EExitCodes?> TryListEnginesAsync(string[] args, CancellationToken? cancellationToken)
    {
        if (args is not {Length: 1} || args.FindParameter("listengines") == null) return null;

        var engineNames = _enginesFactory.EnginesNames; //todo convert this to a method and pass the ct to it

        await _standardOutput.WriteLineAsync($"Available Engines:{nl2}{string.Join(nl, engineNames)}{nl2}");
        
        return EExitCodes.Success;
    }
}