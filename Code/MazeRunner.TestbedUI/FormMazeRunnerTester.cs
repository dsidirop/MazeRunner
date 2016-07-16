using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MazeRunner.Mazes;
using MazeRunner.Shared;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
using MazeRunner.TestbedUI.Helpers;

// ReSharper disable NotAccessedField.Local

namespace MazeRunner.TestbedUI
{
    //todo  using an actual canvas than tlp based canvas might be a better solution overall
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

            _enginesTestbench.AllDone += (s, eaa) => Post(o =>
            {
                txtLog.AppendTextAndScrollToBottom($@"{nl}------------ All Done ----------");
                OnComponentStateChanged(new ComponentStateChanged("testbench.alldone"));
            });
            _enginesTestbench.Commencing += (s, eaa) => Post(o => OnComponentStateChanged(new ComponentStateChanged("testbench.launching")));
            _enginesTestbench.LapStarting += (s, eaa) => Post(o =>
            {
                txtLog.AppendTextAndScrollToBottom($@"{eaa.LapIndex + 1}");
                ccMazeCanvas.ResetCellsToDefaultColors();
            });
            _enginesTestbench.LapConcluded += (s, eaa) => Post(o => txtLog.AppendTextAndScrollToBottom($@"{ConclusionToSymbol[eaa.Status]}  "));
            _enginesTestbench.SingleEngineTestsStarting += (s, eaa) =>
            {
                Post(o => txtLog.Text += $@"{nl2}** Commencing tests on Engine '{eaa.Engine.GetEngineName()}'. Completed Laps: ");
                Thread.Sleep(400);
            };
            _enginesTestbench.SingleEngineTestsCompleted += (s, eaa) =>
            {
                Post(o => txtLog.AppendTextAndScrollToBottom($"{nl2}{eaa.ToString(includeShortestPath: true)}{nl}"));
                Thread.Sleep(1300);
            };

            OnComponentStateChanged(new ComponentStateChanged("form.onload")); //initui
        }

        // ReSharper disable once UnusedParameter.Local   componentstatechanged is there clearly for debugging purposes nothing more
        private void OnComponentStateChanged(ComponentStateChanged ea)
        {
            var testsUnderway = _enginesTestbench.Running;

            btnStop.Enabled = testsUnderway;
            btnStart.Enabled = !testsUnderway;
            nudIterations.Enabled = !testsUnderway;
            nudMovementDelay.Enabled = !testsUnderway;
            lbxkEnginesToBenchmark.Enabled = !testsUnderway;
            saveMazeToolStripMenuItem.Enabled = !testsUnderway;
            loadMazeToolStripMenuItem.Enabled = !testsUnderway;
            generateRandomMazeToolStripMenuItem.Enabled = !testsUnderway;
            reshuffleCurrentMazeToolStripMenuItem.Enabled = !testsUnderway;
        }

        private const int MaxMazeArea = 500;
        private const int MinDelayThreshold = 5;
        private async void btnStart_Click(object sender, EventArgs ea)
        {
            var delay = (int) nudMovementDelay.Value;

            var delayIsSmall = delay < MinDelayThreshold;
            var mazeTooLarge = ccMazeCanvas.Maze.Size.Height * ccMazeCanvas.Maze.Size.Width > MaxMazeArea;
            if (mazeTooLarge)
            {
                ShowMessageSafe($"Live Update of Cells will be disabled because the Maze currently used is too large. Only mazes that have less than {MaxMazeArea} cells get updated live.", "Live Update Disabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var enginesToBenchmark = _mazeRunnersEnginesDataSource.Where(x => x.Selected).Select(x => _enginesFactory.Spawn(x.Name, ccMazeCanvas.Maze)).ToList();

            enginesToBenchmark.ForEach(x =>
            {
                x.StateChanged += (s, eaa) =>
                {
                    if (!mazeTooLarge)
                    {
                        Post(o =>
                        {
                            if (eaa.NewTip != null) ccMazeCanvas.CustomizeCell(eaa.NewTip.Value, NewTipPositionColor, eaa.StepIndex.ToString());
                            if (eaa.OldTip != null) ccMazeCanvas.CustomizeCell(eaa.OldTip.Value, eaa.IsProgressNotBacktracking ? TrajectorySquareColor : InvalidatedSquareColor);
                        });
                    }

                    if (!delayIsSmall) Thread.Sleep(delay);
                };
            });

            _tokenSource?.Dispose(); //order
            _tokenSource = new CancellationTokenSource(); //order
            await _enginesTestbench.RunAsync(enginesToBenchmark, (int) nudIterations.Value, _tokenSource.Token);
        }
        //0 if the delay is set to low there is no point to try and update the ui because the gdi infrastructure simply cant cope to the constant spamming and freezes for quite some time

        private void btnStop_Click(object sender, EventArgs ea) => _tokenSource.Cancel(); // once a tokensource instance gets cancelled its all over for said instance    we thus reinstantiate the tokensource inside btnstart_click

        private void saveMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            var filepath = "";
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = @"Save Maze as";
                saveFileDialog.Filter = $"Maze Files (*{MazefileExtension})|*{MazefileExtension}";
                saveFileDialog.FileName = $"mazemap_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{ccMazeCanvas.Maze.Size.Height:D5}x{ccMazeCanvas.Maze.Size.Width:D5}{MazefileExtension}";
                saveFileDialog.AddExtension = true;
                saveFileDialog.ValidateNames = true;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.InitialDirectory = DesktopDirectory;
                if (saveFileDialog.ShowDialog(this) != DialogResult.OK) return;

                filepath = saveFileDialog.FileName;
            }

            try
            {
                File.WriteAllText(filepath, ccMazeCanvas.Maze.ToAsciiMap());
                using (var formFileGeneratedSuccessfully = new FormNotificationAboutFileOperation())
                {
                    formFileGeneratedSuccessfully.Text = @"Maze Saved Successfully";
                    formFileGeneratedSuccessfully.FilePath = filepath;
                    formFileGeneratedSuccessfully.FileGeneratedSuccessfullyMessage = @"Operation completed successfully";
                    formFileGeneratedSuccessfully.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                ShowMessageSafe($"Failed save maze file to:{nl2}{filepath}{nl2}Please select a different location.", @"Error saving to disk", ex: ex);
            }
        }

        private void loadMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            try
            {
                var filepath = "";
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = @"Select existing backup file";
                    openFileDialog.Filter = $"Maze Files (*{MazefileExtension})|*{MazefileExtension}|All Files(*.*)|*.*";
                    openFileDialog.ValidateNames = true;
                    openFileDialog.CheckPathExists = true;
                    openFileDialog.CheckFileExists = true;
                    openFileDialog.InitialDirectory = DesktopDirectory;
                    if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

                    filepath = openFileDialog.FileName;
                }

                try
                {
                    ccMazeCanvas.Maze = _mazesFactory.FromFile(filepath);
                }
                catch (Exception ex)
                {
                    ShowMessageSafe("Failed to restore backup copy", "Error reading from disk", ex: ex);
                }
            }
            catch (Exception ex)
            {
                ShowMessageSafe(ex.Message, @"Mazefile Loading Failed", ex: ex);
            }
        }

        private void reshuffleCurrentMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            var mazespecs = ccMazeCanvas.Maze.GetMazeSpecs();
            ccMazeCanvas.Maze = _mazesFactory.Random(mazespecs.Width, mazespecs.Height, mazespecs.RoadblockDensity);
        }

        private void generateRandomMazeToolStripMenuItem_Click(object sender, EventArgs ea)
        {
            var mazespecs = ccMazeCanvas.Maze.GetMazeSpecs();
            using (var generateMazeDialog = new FormGenerateNewRandomMaze())
            {
                generateMazeDialog.MazeWidth = mazespecs.Width;
                generateMazeDialog.MazeHeight = mazespecs.Height;
                generateMazeDialog.MazeDensity = mazespecs.RoadblockDensity;
                if (generateMazeDialog.ShowDialog(this) != DialogResult.OK) return;

                ccMazeCanvas.Maze = _mazesFactory.Random(generateMazeDialog.MazeWidth, generateMazeDialog.MazeHeight, generateMazeDialog.MazeDensity);
            }
        }

        private void Post(SendOrPostCallback callback, object data = null) => _synchContext.Post(callback, data);
        //private void Send(SendOrPostCallback callback, object data = null) => _synchContext.Send(callback, data);

        protected DialogResult ShowMessageSafe(string text, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, Exception ex = null, IWin32Window owner = null)
            => MessageBox.Show(owner ?? this, text, caption, buttons, icon, defaultButton);

        [Obfuscation(Exclude = true, ApplyToMembers = true)] //0
        private sealed class EngineEntry
        {
            public string Name { get; set; }
            public bool Selected { get; set; }
        }
        //0 we could have reduce this to [Obfuscation] but it wouldnt be that clear what we are after in terms of obfuscation

        [DebuggerDisplay("{DebuggerDisplayProxy,nq}")]
        internal sealed class ComponentStateChanged
        {
            private readonly string _description;
            public ComponentStateChanged(string description)
            {
                _description = description;
            }

            internal string DebuggerDisplayProxy() => _description;
        }

        static private readonly string nl = U.nl;
        static private readonly string nl2 = U.nl2;
        static private readonly string DesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        static private readonly string MazefileExtension = ".mz";
        static private readonly Color NewTipPositionColor = Color.MediumSeaGreen;
        static private readonly Color TrajectorySquareColor = Color.DarkGreen;
        static private readonly Color InvalidatedSquareColor = Color.Gray;
        static private readonly MazeSpecs KickstartMazeSpecs = new MazeSpecs {Width = 10, Height = 10, RoadblockDensity = 0.1};
        static private readonly ReadOnlyDictionary<ConclusionStatusTypeEnum, string> ConclusionToSymbol = new ReadOnlyDictionary<ConclusionStatusTypeEnum, string>(new Dictionary<ConclusionStatusTypeEnum, string>
        {
            { ConclusionStatusTypeEnum.Crashed, "⚠" },
            { ConclusionStatusTypeEnum.Completed, "✓" },
            { ConclusionStatusTypeEnum.Stopped, "✋" }
        });
    }
}
