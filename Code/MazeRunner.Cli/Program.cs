using System;
using System.Threading.Tasks;
using Autofac;
using MazeRunner.Cli.Engine;
using MazeRunner.Injectors.Autofac;

namespace MazeRunner.Cli;

static internal class Program
{
    static public async Task<int> Main(string[] args)
    {
        await using var injectorContainer = new AutofacInjectorScannerService().TryScanAllAssembliesForInjectionsConfigs().Build();
        await using var injectorContainerScope = injectorContainer.BeginLifetimeScope();

        var cliControllerEngine = injectorContainerScope.Resolve<ICliControllerEngine>();

        try //todo   turn this into a separate service that can be injected into the engine
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
