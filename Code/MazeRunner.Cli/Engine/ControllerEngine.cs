using System.IO;
using System.Threading.Tasks;
using MazeRunner.Contracts;
using MazeRunner.Mazes;

namespace MazeRunner.Cli.Engine;

public partial class ControllerEngine
{
    private readonly TextWriter _standardError;
    private readonly TextWriter _standardOutput;
    private readonly IMazesFactory _mazesFactory;
    private readonly IEnginesFactory _enginesFactory;
    private readonly IEnginesTestbench _enginesTestbench;

    public ControllerEngine(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesTestbench, TextWriter standardOutput, TextWriter standardError)
    {
        _mazesFactory = mazesFactory;
        _standardError = standardError;
        _standardOutput = standardOutput;
        _enginesFactory = enginesFactory;
        _enginesTestbench = enginesTestbench;
    }

    public async Task<int> RunAsync(string[] args)
    {
        var exitcode = (int?) 0;
        if ((exitcode = await TryRunEngineAsync(args)) != null) return exitcode.Value;
        if ((exitcode = TryListEngines(args)) != null) return exitcode.Value;
        if ((exitcode = TryGenerateRandomMaze(args)) != null) return exitcode.Value;

        return PrintUsageMessage(args);
    }
}