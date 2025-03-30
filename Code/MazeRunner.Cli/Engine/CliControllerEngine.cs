using System.IO;
using System.Threading;
using MazeRunner.Contracts;
using MazeRunner.Mazes;

namespace MazeRunner.Cli.Engine;

public partial class CliControllerEngine
{
    public bool HasCancellationBeenAlreadyRequestedOnce { get; private set; }
    
    private readonly TextWriter _standardError;
    private readonly TextWriter _standardOutput;
    private readonly IMazesFactory _mazesFactory;
    private readonly IEnginesFactory _enginesFactory;
    private readonly IEnginesTestbench _enginesTestbench;
    private readonly CancellationTokenSource _masterCancellationTokenSource;

    public CliControllerEngine(
        IEnginesFactory enginesFactory,
        IMazesFactory mazesFactory,
        IEnginesTestbench enginesTestbench,
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
