#pragma warning disable CA2000

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.Injectors.Autofac;
using MazeRunner.TestbedUI.Helpers;

namespace MazeRunner.TestbedUI;

static internal class Program
{
    [STAThread]
    static private async Task Main()
    {
        await using var injectorContainer = new AutofacInjectorScannerService().TryScanAllAssembliesForInjectionsConfigs().Build();
        await using var injectorContainerScope = injectorContainer.BeginLifetimeScope();
        
        _ = Task.Run(injectorContainerScope.Resolve<IGrandMazeRunnersEnginesFactory>().EnsureInitializedOnce); //0 async init

        Application.ThreadException += new ThreadExceptionHandler().Application_ThreadException; //recoverable errors from forms

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(injectorContainerScope.Resolve<FormMazeRunnerTester>());

        //0 as a small optimization we force the factory to load and scan assemblies asynchronously so that the form may have the engine-names
        //  readily available a bit down the road without stalling
    }

    private class ThreadExceptionHandler
    {
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs ea)
        {
            var oex = ea.Exception;
            if (oex is OperationCanceledException) return; //stop button

            using var form = new FormUnhandledException(oex);
            
            form.ShowDialog();
        }
    }
}