using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Enums;

namespace MazeRunner.Cli.Engine;

public interface ICliControllerEngine
{
    Task<EExitCodes> ProcessCliArgsAsync(string[] args, CancellationToken? cancellationToken = null);
    
    bool Cancel();
}