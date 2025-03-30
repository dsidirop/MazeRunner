using System;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Cli.Engine;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Mazes;

namespace MazeRunner.Cli;

static internal class Program
{
    static public async Task<int> Main(string[] args)
    {
        var hasCancellationBeenRequestedOnce = false;

        using var cancellationTokenSource = new CancellationTokenSource();

        Console.CancelKeyPress += Console_CancelKeyComboPressed_;

        var exitCode = await new CliControllerEngine(
                mazesFactory: new MazesFactory(),
                enginesFactory: EnginesFactorySingleton.I,
                enginesTestbench: new EnginesTestbench(),

                standardError: Console.Error,
                standardOutput: Console.Out
            )
            .RunAsync(
                args: args,
                cancellationToken: cancellationTokenSource.Token
            );

        return Environment.ExitCode = (int) exitCode;

        void Console_CancelKeyComboPressed_(object sender, ConsoleCancelEventArgs ea)
        {
            if (hasCancellationBeenRequestedOnce)
            {
                ea.Cancel = false;
                Console.Out.WriteAsync("Exiting immediately ... ");
                return; //30
            }

            ea.Cancel = true; //35
            hasCancellationBeenRequestedOnce = true;

            Console.Out.WriteAsync("Cancelling ... ");

            // ReSharper disable once AccessToDisposedClosure
            cancellationTokenSource.Cancel();
        }

        //30   if the user presses Ctrl+C twice we want to exit the program immediately
        //35   prevent the process from terminating because we want to exit smoothly in our own accord
    }
}
