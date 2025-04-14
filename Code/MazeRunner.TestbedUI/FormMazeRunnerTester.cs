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
using MazeRunner.Contracts;
using MazeRunner.Contracts.Events;
using MazeRunner.Mazes;
using MazeRunner.TestbedUI.Helpers;
using MazeRunner.Utils;

// ReSharper disable NotAccessedField.Local

namespace MazeRunner.TestbedUI;

//todo  using an actual canvas than a tlp based canvas might be a better solution overall
public partial class FormMazeRunnerTester : Form
{
    private CancellationTokenSource _tokenSource;
    private readonly IMazesFactory _mazesFactory;
    private readonly IEnginesFactory _enginesFactory;
    private readonly IEnginesTestbench _enginesTestbench;
    private readonly SynchronizationContext _syncContext;
    private readonly BindingList<EngineEntry> _mazeRunnersEnginesDataSource;

    public FormMazeRunnerTester(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesTestbench)
    {
        InitializeComponent();

        _syncContext = SynchronizationContext.Current ?? new SynchronizationContext();
        _mazesFactory = mazesFactory;
        _enginesFactory = enginesFactory;
        _enginesTestbench = enginesTestbench;
        _mazeRunnersEnginesDataSource = []; //order

        _lbxkEnginesToBenchmark.DataSource = _mazeRunnersEnginesDataSource; //order
        _lbxkEnginesToBenchmark.ValueMember = nameof(EngineEntry.Selected); //order
        _lbxkEnginesToBenchmark.DisplayMember = nameof(EngineEntry.Name); //order
    }
    
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        _tokenSource?.Dispose();
            
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnLoad(EventArgs ea)
    {
        _ccMazeCanvas.Maze = _mazesFactory.SpawnRandom(KickstartMazeSpecs.Width, KickstartMazeSpecs.Height, KickstartMazeSpecs.RoadblockDensity);

        _enginesFactory.EnginesNames.ForEach(x => _mazeRunnersEnginesDataSource.Add(new EngineEntry {Selected = true, Name = x})); //0 engines-names order
        _mazeRunnersEnginesDataSource.Each((x, i) => _lbxkEnginesToBenchmark.SetItemChecked(i, x.Selected)); //order

        _lnkClearLogs.LinkClicked += lnkClearLogs_LinkClicked_; //order
        _lbxkEnginesToBenchmark.ItemCheck += lbxkEnginesToBenchmark_ItemCheckStatusChanged_; //order

        _enginesTestbench.Commencing += EnginesTestbench_Commencing_;
        {
            _enginesTestbench.SpecificEngineTestsStarting += EnginesTestbench_SpecificEngineTestsStarting_;
        
            _enginesTestbench.SpecificEngineLapStarting += EnginesTestbench_SpecificEngineLapStarting_;
            _enginesTestbench.SpecificEngineLapConcluded += EnginesTestbench_SpecificEngineLapConcluded_;

            _enginesTestbench.SpecificEngineTestsCompleted += EnginesTestbench_SpecificEngineTestsCompleted_;    
        }
        _enginesTestbench.AllDone += EnginesTestbench_AllDone_;

        OnComponentStateChanged(new ComponentStateChanged("form.onload")); //init ui
        return;

        void lnkClearLogs_LinkClicked_(object o, LinkLabelLinkClickedEventArgs linkLabelLinkClickedEventArgs)
        {
            txtLog.Clear();
        }

        void lbxkEnginesToBenchmark_ItemCheckStatusChanged_(object _, ItemCheckEventArgs ea_)
        {
            _mazeRunnersEnginesDataSource[ea_.Index].Selected = ea_.NewValue == CheckState.Checked;
        }

        void EnginesTestbench_AllDone_(object o, AllDoneEventArgs allDoneEventArgs)
        {
            Post(PostCallback_);
            return;

            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($@"{nl}------------ All Done ----------");
                OnComponentStateChanged(new ComponentStateChanged("testbench.alldone"));
            }
        }

        void EnginesTestbench_Commencing_(object o, CommencingEventArgs ea_)
        {
            Post(PostCallback_);
            return;

            void PostCallback_(object _)
            {
                txtLog.Text += $@"{nl2}** Commencing tests on {ea_.Engines.Count} engines with {ea_.LapsPerEngine} laps per engine ... ";
                OnComponentStateChanged(new ComponentStateChanged("testbench.launching"));
            }
        }
        
        void EnginesTestbench_SpecificEngineTestsStarting_(object _, SpecificEngineTestsStartingEventArgs ea_)
        {
            Post(PostCallback_);
            Thread.Sleep(400);
            return;
            
            void PostCallback_(object _)
            {
                txtLog.Text += $@"{nl2}**** Commencing tests on Engine '{ea_.Engine.GetEngineName()}'. Completed Laps: ";
            }
        }
        
        void EnginesTestbench_SpecificEngineLapStarting_(object _, SpecificEngineLapStartingEventArgs ea_)
        {
            Post(PostCallback_);
            return;

            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($@"{ea_.LapIndex + 1}");
                _ccMazeCanvas.ResetCellsToDefaultColors();
            }
        }
        
        void EnginesTestbench_SpecificEngineLapConcluded_(object _, SpecificEngineLapConcludedEventArgs ea_)
        {
            Post(PostCallback_);
            return;

            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($@"{ConclusionToSymbol[ea_.Status]}  ");
            }
        }
        
        void EnginesTestbench_SpecificEngineTestsCompleted_(object _, SpecificEngineTestsCompletedEventArgs ea_)
        {
            Post(PostCallback_);
            Thread.Sleep(1300);
            return;
            
            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($"{nl2}{ea_.ToStringy(includeShortestPath: true)}{nl}");
            }
        }

        //0 the property engines-names will cause the factory to perform a onetime initialization onthefly which involves loading assemblies and so on   this can potentially prove
        //  time-consuming thus stalling the display of the form   by delegating the initialization process to a subthread we make the display of the form snappier in this regard
    }

    static public readonly ReadOnlyDictionary<ConclusionStatusTypeEnum, string> ConclusionToSymbol = new Dictionary<ConclusionStatusTypeEnum, string>
    {
        { ConclusionStatusTypeEnum.Stopped, "✋" },
        { ConclusionStatusTypeEnum.Crashed, "⚠️" },
        { ConclusionStatusTypeEnum.Completed, "✅️" },
    }.AsReadOnly();

    // ReSharper disable once UnusedParameter.Local   componentstatechanged is there clearly for debugging purposes nothing more
    private void OnComponentStateChanged(ComponentStateChanged ea)
    {
        var testsUnderway = _enginesTestbench.Running;

        btnStop.Enabled = testsUnderway;
        btnStart.Enabled = !testsUnderway;
        nudIterations.Enabled = !testsUnderway;
        nudMovementDelay.Enabled = !testsUnderway;
        _lbxkEnginesToBenchmark.Enabled = !testsUnderway;
        saveMazeToolStripMenuItem.Enabled = !testsUnderway;
        loadMazeToolStripMenuItem.Enabled = !testsUnderway;
        generateRandomMazeToolStripMenuItem.Enabled = !testsUnderway;
        reshuffleCurrentMazeToolStripMenuItem.Enabled = !testsUnderway;
    }

    private const int MaxMazeArea = 500;
    private const int MinDelayThreshold = 5;
    private async void btnStart_Click(object sender, EventArgs ea)
    {
        try
        {
            var delay = (int) nudMovementDelay.Value;

            var delayIsSmall = delay < MinDelayThreshold;
            var mazeTooLarge = _ccMazeCanvas.Maze.Size.Height * _ccMazeCanvas.Maze.Size.Width > MaxMazeArea;
            if (mazeTooLarge)
            {
                ShowMessageSafe($"Live Update of Cells will be disabled because the Maze currently used is too large. Only mazes that have less than {MaxMazeArea} cells get updated live.", "Live Update Disabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _tokenSource?.Dispose(); //order
            _tokenSource = new CancellationTokenSource(); //order

            var enginesToBenchmark = _mazeRunnersEnginesDataSource
                .Where(x => x.Selected)
                .Select(x => _enginesFactory.Spawn(x.Name, _ccMazeCanvas.Maze))
                .Select(x =>
                {
                    x.StateChanged += Engine_OnStateChanged_;
                    return x;
                })
                .ToArray();

            try
            {
                await _enginesTestbench.RunAsync(enginesToBenchmark, (int) nudIterations.Value, _tokenSource.Token);
            }
            finally
            {
                enginesToBenchmark.ForEach(x => x.StateChanged -= Engine_OnStateChanged_);
            }

            void Engine_OnStateChanged_(object _, StateChangedEventArgs eaa)
            {
                if (!mazeTooLarge)
                {
                    Post(PostCallback_);
                }

                if (!delayIsSmall) Thread.Sleep(delay); //1 todo  github#22   dont use task.delay() here because it doesnt work

                return;
                
                void PostCallback_(object _)
                {
                    try
                    {
                        _ccMazeCanvas.tlpMesh.SuspendLayout();
                        _ccMazeCanvas.tlpMesh.SuspendDrawing();
                        
                        if (eaa.NewTip != null) _ccMazeCanvas.CustomizeCell(eaa.NewTip.Value, NewTipPositionColor, eaa.StepIndex.ToString());
                        if (eaa.OldTip != null) _ccMazeCanvas.CustomizeCell(eaa.OldTip.Value, eaa.IsProgressNotBacktracking ? TrajectorySquareColor : InvalidatedSquareColor);
                    }
                    finally
                    {
                        _ccMazeCanvas.tlpMesh.ResumeDrawing();
                        _ccMazeCanvas.tlpMesh.ResumeLayout();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
                return;

            using (var form = new FormUnhandledException(ex))
            {
                form.ShowDialog();
            }
        }
    }
    //0 if the delay is set to low there is no point to try and update the ui because the gdi infrastructure simply cant cope to the constant spamming and freezes for quite some time
    //1 todo  githubticket#22  using thread-sleep causes the engine performance to degrade and the results reported to be distorted    read the ticket on some possible solutions for this issue

    private void btnStop_Click(object sender, EventArgs ea) => _tokenSource.Cancel(); // once a token-source instance gets cancelled it is all over for said instance    we thus reinstantiate the token-source inside btnstart_click

    private void saveMazeToolStripMenuItem_Click(object sender, EventArgs ea)
    {
        var filepath = "";
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Title = @"Save Maze as";
            saveFileDialog.Filter = $@"Maze Files (*{MazefileExtension})|*{MazefileExtension}";
            saveFileDialog.FileName = $"mazemap_{DateTime.Now:yyyyMMddHHmmss}_{_ccMazeCanvas.Maze.Size.Height:D5}x{_ccMazeCanvas.Maze.Size.Width:D5}{MazefileExtension}";
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
            File.WriteAllText(filepath, _ccMazeCanvas.Maze.ToAsciiMap());
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

    private async void loadMazeToolStripMenuItem_Click(object sender, EventArgs ea)
    {
        try
        {
            var filepath = "";
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = @"Select existing backup file";
                openFileDialog.Filter = $@"Maze Files (*{MazefileExtension})|*{MazefileExtension}|All Files(*.*)|*.*";
                openFileDialog.ValidateNames = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.InitialDirectory = DesktopDirectory;
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

                filepath = openFileDialog.FileName;
            }

            try
            {
                _ccMazeCanvas.Maze = await _mazesFactory.FromFileAsync(filepath);
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
        var mazespecs = _ccMazeCanvas.Maze.GetMazeSpecs();
        _ccMazeCanvas.Maze = _mazesFactory.SpawnRandom(mazespecs.Width, mazespecs.Height, mazespecs.RoadblockDensity);
    }

    private void generateRandomMazeToolStripMenuItem_Click(object sender, EventArgs ea)
    {
        var mazespecs = _ccMazeCanvas.Maze.GetMazeSpecs();
        using (var generateMazeDialog = new FormGenerateNewRandomMaze())
        {
            generateMazeDialog.MazeWidth = mazespecs.Width;
            generateMazeDialog.MazeHeight = mazespecs.Height;
            generateMazeDialog.MazeDensity = mazespecs.RoadblockDensity;
            if (generateMazeDialog.ShowDialog(this) != DialogResult.OK) return;

            _ccMazeCanvas.Maze = _mazesFactory.SpawnRandom(generateMazeDialog.MazeWidth, generateMazeDialog.MazeHeight, generateMazeDialog.MazeDensity);
        }
    }

    private void Post(SendOrPostCallback callback, object data = null) => _syncContext.Post(callback, data);
    //private void Send(SendOrPostCallback callback, object data = null) => _syncContext.Send(callback, data);

    protected DialogResult ShowMessageSafe(string text, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, Exception ex = null, IWin32Window owner = null)
        => MessageBox.Show(owner ?? this, text, caption, buttons, icon, defaultButton);

    [Obfuscation(Exclude = true, ApplyToMembers = true)] //0
    private sealed class EngineEntry
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
        
        //0 we could have reduced this to [Obfuscation] but it would not be that clear what we are after in terms of obfuscation
    }

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

    private const string MazefileExtension = ".mz";
    
    static private readonly string nl = U.nl;
    static private readonly string nl2 = U.nl2;
    static private readonly string DesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    static private readonly Color NewTipPositionColor = Color.MediumSeaGreen;
    static private readonly Color TrajectorySquareColor = Color.DarkGreen;
    static private readonly Color InvalidatedSquareColor = Color.Gray;
    static private readonly MazeSpecs KickstartMazeSpecs = new() {Width = 10, Height = 10, RoadblockDensity = 0.1};
}