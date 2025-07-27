using System.IO;
using System.Threading;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.Mazes.Contracts;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    public bool HasCancellationBeenAlreadyRequestedOnce { get; private set; }
    
    private readonly TextWriter _standardError;
    private readonly TextWriter _standardOutput;
    private readonly IMazesFactory _mazesFactory;
    private readonly IEnginesTestbench _enginesTestbench;
    private readonly IGrandMazeRunnersEnginesFactory _enginesFactory;
    private readonly CancellationTokenSource _masterCancellationTokenSource;

    public CliControllerEngine(
        IMazesFactory mazesFactory,
        IEnginesTestbench enginesTestbench,
        IGrandMazeRunnersEnginesFactory enginesFactory,
        TextWriter standardOutput,
        TextWriter standardError,
        CancellationToken? cancellationToken = null
    )
    {
        _mazesFactory = mazesFactory;
        _standardError = standardError;
        _standardOutput = standardOutput;
        _enginesFactory = enginesFactory;
        _enginesTestbench = enginesTestbench;

        _masterCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken ?? CancellationToken.None);
    }
}
