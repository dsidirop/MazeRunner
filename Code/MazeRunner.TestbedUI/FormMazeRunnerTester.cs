using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
// ReSharper disable NotAccessedField.Local

namespace MazeRunner.TestbedUI
{
    public partial class FormMazeRunnerTester : Form
    {
        private CancellationTokenSource _tokenSource;
        private readonly IMazesFactory _mazesFactory;
        private readonly IEnginesFactory _enginesFactory;
        private readonly IEnginesTestbench _enginesTestbench;
        private readonly SynchronizationContext _synchContext;
        private readonly BindingList<EngineEntry> _mazeRunnersEnginesDataSource;

        public FormMazeRunnerTester(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesTestbench)
        {
            InitializeComponent();

            _synchContext = SynchronizationContext.Current ?? new SynchronizationContext();
            _mazesFactory = mazesFactory;
            _enginesFactory = enginesFactory;
            _enginesTestbench = enginesTestbench;
            _mazeRunnersEnginesDataSource = new BindingList<EngineEntry>(); //order

            lbxkEnginesToBenchmark.DataSource = _mazeRunnersEnginesDataSource; //order
            lbxkEnginesToBenchmark.ValueMember = nameof(EngineEntry.Selected); //order
            lbxkEnginesToBenchmark.DisplayMember = nameof(EngineEntry.Name); //order
        }
        //0 the property enginesnames will cause the factory to perform a onetime initialization onthefly which involves loading assemblies and so on   this can potentially prove
        //  time consuming thus stalling the display of the form   by delegating the initialization process to a subthread we make the display of the form snappier in this regard

        protected override void OnLoad(EventArgs ea)
        {
            ccMazeCanvas.Maze = _mazesFactory.Random(KickstartMazeSpecs.Width, KickstartMazeSpecs.Height, KickstartMazeSpecs.RoadblockDensity);

            lnkClearLogs.LinkClicked += (s, eaa) => txtLog.Clear();

            _enginesFactory.EnginesNames.ForEach(x => _mazeRunnersEnginesDataSource.Add(new EngineEntry {Selected = true, Name = x})); //order
            _mazeRunnersEnginesDataSource.Each((x, i) => lbxkEnginesToBenchmark.SetItemChecked(i, x.Selected)); //order
            lbxkEnginesToBenchmark.ItemCheck += (s, eaa) => _mazeRunnersEnginesDataSource[eaa.Index].Selected = eaa.NewValue == CheckState.Checked; //order
            
            _enginesTestbench.AllDone += (s, eaa) => _synchContext.Post(o => txtLog.AppendTextAndScrollToBottom($@"{nl}------------ All Done ----------"));
            _enginesTestbench.LapStarting += (s, eaa) => _synchContext.Post(o => ccMazeCanvas.ResetCellsToDefaultColors());
            _enginesTestbench.LapConcluded += (s, eaa) => _synchContext.Post(o => txtLog.Text += $@"{eaa.LapIndex + 1} ");
            _enginesTestbench.SingleEngineTestsStarting += (s, eaa) =>
            {
                _synchContext.Post(o => txtLog.Text += $@"{nl}{nl}** Commencing tests on Engine '{eaa.Engine.GetType().Name}'. Lap-count: ");
                Thread.Sleep(400);
            };
            _enginesTestbench.SingleEngineTestsCompleted += (s, eaa) =>
            {
                _synchContext.Post(o => txtLog.AppendTextAndScrollToBottom(
                    $"{nl}{nl}" +
                    $"Engine: {eaa.Engine.GetType().Name}{nl}" +
                    $"Number of runs: {eaa.Repetitions} (smooth runs: {eaa.Repetitions - eaa.Crashes}, crashes: {eaa.Crashes}){nl}" +
                    $"Path-lengths (Best / Worst / Average): {eaa.BestPathLength} / {eaa.WorstPathLength} / {eaa.AveragePathLength}{nl}" +
                    $"Time-durations (Best / Worst / Average): {eaa.BestTimePerformance.TotalMilliseconds}ms / {eaa.WorstTimePerformance.TotalMilliseconds}ms / {eaa.AverageTimePerformance.TotalMilliseconds}ms{nl}"));

                Thread.Sleep(1300);
            };
        }

        private void btnStart_Click(object sender, EventArgs ea)
        {
            var enginesToBenchmark = _mazeRunnersEnginesDataSource.Where(x => x.Selected).Select(x => _enginesFactory.Spawn(x.Name, ccMazeCanvas.Maze)).ToList();
            enginesToBenchmark.ForEach(x => x.StateChanged += (s, eaa) =>
            {
                _synchContext.Post(o =>
                {
                    if (eaa.NewTip != null) ccMazeCanvas.CustomizeCell(eaa.NewTip.Value, NewTipPositionColor, eaa.StepIndex.ToString());
                    if (eaa.OldTip != null) ccMazeCanvas.CustomizeCell(eaa.OldTip.Value, eaa.IsProgressNotBacktracking ? TrajectorySquareColor : InvalidatedSquareColor);
                }, null);

                var delaySnapshot = (int) nudMovementDelay.Value;
                if (delaySnapshot > 0) Thread.Sleep(delaySnapshot);
            });

            _tokenSource?.Dispose(); //order
            _tokenSource = new CancellationTokenSource(); //order
            _enginesTestbench.RunAsync(enginesToBenchmark, (int) nudIterations.Value, _tokenSource.Token); //order
        }

        private void btnStop_Click(object sender, EventArgs ea) => _tokenSource.Cancel();
        // once a tokensource gets cancelled its all over for it    thus we reinstantiate the tokensource inside btnstart_click

        private void reshuffleCurrentMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            var density = ccMazeCanvas.Maze.RoadblocksCount / (((double)ccMazeCanvas.Maze.Size.Width) * ccMazeCanvas.Maze.Size.Height);
            ccMazeCanvas.Maze = _mazesFactory.Random(ccMazeCanvas.Maze.Size.Width, ccMazeCanvas.Maze.Size.Height, density);
        }

        private void generateRandomMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            //todo show form
        }

        private sealed class MazeSpecs
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
        static private readonly Color NewTipPositionColor = Color.MediumSeaGreen;
        static private readonly Color TrajectorySquareColor = Color.DarkGreen;
        static private readonly Color InvalidatedSquareColor = Color.Gray;
        static private readonly MazeSpecs KickstartMazeSpecs = new MazeSpecs {Width = 10, Height = 10, RoadblockDensity = 0.1};
    }

    static internal class ControlsUtilsX
    {
        static internal void AppendTextAndScrollToBottom(this TextBox textbox, string textToAppend)
        {
            textbox.Text += textToAppend;
            if (!textbox.Visible) return;

            textbox.SelectionStart = textbox.TextLength;
            textbox.ScrollToCaret();
        }

        static internal void Post(this SynchronizationContext context, SendOrPostCallback callback) => context.Post(callback, null);
        static internal void Send(this SynchronizationContext context, SendOrPostCallback callback) => context.Send(callback, null);
    }
}
