using System;
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
        var cliControllerEngine = new CliControllerEngine(
            mazesFactory: new MazesFactory(),
            enginesFactory: EnginesFactorySingleton.I,
            enginesTestbench: new EnginesTestbench(),

            standardError: Console.Error,
            standardOutput: Console.Out
        );

        try
        {
            Console.CancelKeyPress += Console_CancelKeyComboPressed_;

            var exitCode = await cliControllerEngine.ProcessCliArgsAsync(args);
            
            return Environment.ExitCode = (int) exitCode;
        }
        finally
        {
            Console.CancelKeyPress -= Console_CancelKeyComboPressed_; //just in case
        }

        void Console_CancelKeyComboPressed_(object sender, ConsoleCancelEventArgs ea)
        {
            ea.Cancel = true; //vital in order to prevent the process from exiting immediately so that we can cancel smoothly
            cliControllerEngine.Cancel();
        }
    }
}
