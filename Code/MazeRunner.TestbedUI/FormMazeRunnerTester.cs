using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
// ReSharper disable NotAccessedField.Local

namespace MazeRunner.TestbedUI
{
    public partial class FormMazeRunnerTester : Form
    {
        private RandomMazeSettings _lastRandomMazeSettings = new RandomMazeSettings {Width = 20, Height = 15, RoadblockDensity = 0.05};

        private readonly IMazesFactory _mazesFactory;
        private readonly IEnginesFactory _enginesFactory;
        private readonly IEnginesTestbench _enginesTestbench;
        
        public FormMazeRunnerTester(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesTestbench)
        {
            InitializeComponent();

            _mazesFactory = mazesFactory;
            _enginesFactory = enginesFactory;
            _enginesTestbench = enginesTestbench; // ReSharper disable once PossibleNullReferenceException
        }

        private BindingList<EngineEntry> _enginesDataSource;
        protected override void OnShown(EventArgs eawef)
        {
            _ccMazeCanvas.Maze = _mazesFactory.Random(_lastRandomMazeSettings.Width, _lastRandomMazeSettings.Height, _lastRandomMazeSettings.RoadblockDensity);

            lbxkEnginesToBenchmark.ValueMember = nameof(EngineEntry.Selected); //order
            lbxkEnginesToBenchmark.DisplayMember = nameof(EngineEntry.Name); //order
            lbxkEnginesToBenchmark.DataSource = _enginesDataSource = new BindingList<EngineEntry>(_enginesFactory.EnginesNames.Select(x => new EngineEntry {Selected = true, Name = x}).ToList()); //order
            _enginesDataSource.Each((x, i) => lbxkEnginesToBenchmark.SetItemChecked(i, x.Selected)); //order
            lbxkEnginesToBenchmark.ItemCheck += (s, eaa) => _enginesDataSource[eaa.Index].Selected = eaa.NewValue == CheckState.Checked; //order

            _enginesTestbench.AllDone += (s, eaa) =>
            {
                txtLog.Text += $@"{nl}------------ All Done ----------";
                txtLog.ScrollToBottom();
            };
            _enginesTestbench.LapCompleted += (s, eaa) => txtLog.Text += $@"{eaa.LapIndex + 1} ";
            _enginesTestbench.SingleEngineTestsStarting += (s, eaa) => txtLog.Text += $@"{nl}{nl}** Commencing tests on Engine '{eaa.Engine.GetType().Name}'. Lap-count: ";
            _enginesTestbench.SingleEngineTestsCompleted += (s, eaa) =>
            {
                txtLog.Text +=
                    $"{nl}{nl}" +
                    $"Engine: {eaa.Engine.GetType().Name}{nl}" +
                    $"Number of runs: {eaa.Repetitions} (smooth runs: {eaa.Repetitions - eaa.Crashes}, crashes: {eaa.Crashes}){nl}" +
                    $"Path-lengths (Best / Worst / Average): {eaa.BestPathLength} / {eaa.WorstPathLength} / {eaa.AveragePathLength}{nl}" +
                    $"Time-durations (Best / Worst / Average): {eaa.BestTimePerformance.TotalMilliseconds}ms / {eaa.WorstTimePerformance.TotalMilliseconds}ms / {eaa.AverageTimePerformance.TotalMilliseconds}ms{nl}";
                txtLog.ScrollToBottom();
            };
        }

        private void btnStart_Click(object sender, EventArgs ea)
        {
            var enginesToBenchmark = _enginesDataSource.Where(x => x.Selected).Select(x => _enginesFactory.Spawn(x.Name, _ccMazeCanvas.Maze)).ToList();

            _enginesTestbench.Run(enginesToBenchmark, (int) nudIterations.Value); //todo async
        }

        private void btnStop_Click(object sender, EventArgs ea)
        {
            _enginesTestbench.Stop(); //todo
        }

        private void reshuffleCurrentRandomMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            _ccMazeCanvas.Maze = _mazesFactory.Random(_lastRandomMazeSettings.Width, _lastRandomMazeSettings.Height, _lastRandomMazeSettings.RoadblockDensity);
        }

        private void generateRandomMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            //todo show form
        }

        private sealed class RandomMazeSettings
        {
            public int Width;
            public int Height;
            public double RoadblockDensity;
        }

        [Obfuscation(Exclude = true, ApplyToMembers = true)] //0
        private sealed class EngineEntry
        {
            public string Name { get; set; }
            public bool Selected { get; set; }
        }
        //0 we could have reduce this to [Obfuscation] but it wouldnt be that clear what we are after in terms of obfuscation

        static private readonly string nl = Utilities.nl;
    }

    static internal class ControlsUtilsX
    {
        static internal void ScrollToBottom(this TextBox textbox)
        {
            if (!textbox.Visible) return;

            textbox.SelectionStart = textbox.TextLength;
            textbox.ScrollToCaret();
        }
    }
}
