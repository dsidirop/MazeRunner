using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Mazes;
using MazeRunner.TestbedUI.Helpers;

namespace MazeRunner.TestbedUI;

static internal class Program
{
    [STAThread]
    static private void Main()
    {
        Task.Factory.StartNew(() => EnginesFactorySingleton.I.EnginesNames); //0 async init

        Application.ThreadException += new ThreadExceptionHandler().Application_ThreadException; //recoverable errors from forms

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormMazeRunnerTester(EnginesFactorySingleton.I, new MazesFactory(), new EnginesTestbench()));
            
        //0 as a small optimization we force the factory to load and scan assemblies asynchronously so that the form may have the enginenames
        //  readily available a bit down the road without stalling
    }

    private class ThreadExceptionHandler
    {
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs ea)
        {
            var oex = ea.Exception;
            if (oex is OperationCanceledException) return; //stop button
                
            using (var form = new FormUnhandledException(oex))
            {
                form.ShowDialog();
            }
        }
    }
}