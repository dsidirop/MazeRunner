using System;
using System.Windows.Forms;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Mazes;

namespace MazeRunner.TestbedUI
{
    static internal class Program
    {
        [STAThread]
        static private void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMazeRunnerTester(EnginesFactorySingleton.I, new MazesFactory(), new EnginesTestbench()));
        }
    }
}
