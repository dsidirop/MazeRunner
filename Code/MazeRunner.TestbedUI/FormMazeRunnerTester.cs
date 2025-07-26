using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MazeRunner.Contracts;
using MazeRunner.Contracts.Events;
using MazeRunner.Engines.Contracts;
using MazeRunner.Engines.Contracts.Events;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.EnginesFactory.Contracts.Events;
using MazeRunner.Mazes;
using MazeRunner.TestbedUI.Helpers;
using MazeRunner.Utils;
using MazeRunner.Utils.Reactive;

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
    
    private IScheduler _subscriptionSchedulerFor_UI;
    private IScheduler _subscriptionSchedulerFor_Logging;
    
    private IDisposable _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_UI;
    private IDisposable _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_Logging;
    private readonly BindingList<EngineEntry> _mazeRunnersEnginesDataSource;
    
    private Subject<(object Sender, IMazeRunnerEventArgs EventArgs)> _mazeRunnerBenchmarkingUpdatingEventsSubject;

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
        _mazeRunnerBenchmarkingUpdatingEventsSubject?.Dispose();
        _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_UI?.Dispose();
        _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_Logging?.Dispose();

        (_subscriptionSchedulerFor_UI as IDisposable)?.Dispose();
        //(_subscriptionSchedulerFor_Logging as IDisposable)?.Dispose(); //dont
        
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

        _enginesTestbench.BenchmarkingCommencing += EnginesTestbench_BenchmarkingCommencing_;
        {
            _enginesTestbench.SpecificEngineTestsSuiteStarting += EnginesTestbench_SpecificEngineTestsSuiteStarting_;
        
            _enginesTestbench.SpecificEngineSingleLapStarting += EnginesTestbench_SpecificEngineSingleLapStarting_;
            _enginesTestbench.SpecificEngineSingleLapConcluded += EnginesTestbench_SpecificEngineSingleLapConcluded_;

            _enginesTestbench.SpecificEngineTestsSuiteCompleted += EnginesTestbench_SpecificEngineTestsSuiteCompleted_;    
        }
        _enginesTestbench.AllBenchmarkingsDone += EnginesTestbench_AllBenchmarkingsDone_;

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

        void EnginesTestbench_AllBenchmarkingsDone_(object sender_, AllBenchmarkingsDoneEventArgs ea_)
        {
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnCompleted();
        }

        void EnginesTestbench_BenchmarkingCommencing_(object sender_, BenchmarkingCommencingEventArgs ea_)
        {
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
        }
        
        void EnginesTestbench_SpecificEngineTestsSuiteStarting_(object sender_, SpecificEngineTestsSuiteStartingEventArgs ea_)
        {
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
        }
        
        void EnginesTestbench_SpecificEngineSingleLapStarting_(object sender_, SpecificEngineSingleLapStartingEventArgs ea_)
        {
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
        }
        
        void EnginesTestbench_SpecificEngineSingleLapConcluded_(object sender_, SpecificEngineSingleLapConcludedEventArgs ea_)
        {
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
        }
        
        void EnginesTestbench_SpecificEngineTestsSuiteCompleted_(object sender_, SpecificEngineTestsSuiteCompletedEventArgs ea_)
        {
            _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
        }

        //0 the property engines-names will cause the factory to perform a onetime initialization onthefly which involves loading assemblies and so on   this can potentially prove
        //  time-consuming thus stalling the display of the form   by delegating the initialization process to a subthread we make the display of the form snappier in this regard
    }

    static public readonly FrozenDictionary<ConclusionStatusTypeEnum, string> ConclusionToSymbol = new Dictionary<ConclusionStatusTypeEnum, string>(3)
    {
        { ConclusionStatusTypeEnum.Stopped, "✋" },
        { ConclusionStatusTypeEnum.Crashed, "⚠️" },
        { ConclusionStatusTypeEnum.Completed, "✅️" },
    }.ToFrozenDictionary();

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
    
    private bool _isDelayTooSmall;
    private bool _isMazeTooLarge;
    private const int MaxMazeArea = 500;
    private const int MinDelayThreshold = 5;
    private async void btnStart_Click(object sender, EventArgs ea)
    {
        try
        {
            var delay = (int) nudMovementDelay.Value;

            _isDelayTooSmall = delay < MinDelayThreshold;
            _isMazeTooLarge = _ccMazeCanvas.Maze.Size.Height * _ccMazeCanvas.Maze.Size.Width > MaxMazeArea;
            if (_isMazeTooLarge)
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

            _mazeRunnerBenchmarkingUpdatingEventsSubject?.Dispose();
            _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_UI?.Dispose();
            _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_Logging?.Dispose();
            
            _subscriptionSchedulerFor_UI ??= new EventLoopScheduler(); //        we can safely reuse the schedulers once we ensure they have been created once
            _subscriptionSchedulerFor_Logging ??= TaskPoolScheduler.Default; //  ideal for logging purposes

            _mazeRunnerBenchmarkingUpdatingEventsSubject = new Subject<(object Sender, IMazeRunnerEventArgs EventArgs)>();

            _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_UI = _mazeRunnerBenchmarkingUpdatingEventsSubject
                .ObserveOn(_subscriptionSchedulerFor_UI)
                .SubscribeAndHandleAllExceptions(
                    onNext: x => MazeRunnerBenchmarkingUpdatingEventsStream_NextForUI(x.Sender, x.EventArgs),
                    onError: MazeRunnerBenchmarkingUpdatesSubjectSubscriber_Errored
                );
            
            _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_Logging = _mazeRunnerBenchmarkingUpdatingEventsSubject
                .ObserveOn(_subscriptionSchedulerFor_Logging)
                .SubscribeAndHandleAllExceptions(
                    onNext: x => MazeRunnerBenchmarkingUpdatingEventsStream_NextForLogging(x.Sender, x.EventArgs),
                    onError: MazeRunnerBenchmarkingUpdatesSubjectSubscriber_Errored
                );

            try
            {
                await _enginesTestbench.RunAsync(enginesToBenchmark, (int) nudIterations.Value, _tokenSource.Token);
            }
            finally
            {
                enginesToBenchmark.ForEach(x => x.StateChanged -= Engine_OnStateChanged_);
            }

            void Engine_OnStateChanged_(object sender_, StateChangedEventArgs ea_)
            {
                _mazeRunnerBenchmarkingUpdatingEventsSubject.OnNext((sender_, ea_));
            }
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                OnBenchmarkingCancelled();
                return;
            }
            
            HandlePossibleOops(ex);
        }
        
        //0 if the delay is set to low there is no point to try and update the ui because the gdi infrastructure simply cant cope to the constant spamming and freezes for quite some time
    }

    private void OnBenchmarkingCancelled()
    {
        OnComponentStateChanged(new ComponentStateChanged("testbench.cancelled"));
                
        txtLog.AppendTextAndScrollToBottom($@"{nl}Cancelled!");
                
        _mazeRunnerBenchmarkingUpdatingEventsSubject?.Dispose();
        _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_UI?.Dispose();
        _subscriptionOnMazeRunnerBenchmarkingEventsStreamFor_Logging?.Dispose();

        // _subscriptionSchedulerFor_UI?.Dispose();        //nah  no need to dispose   the schedulers are reusable
        // _subscriptionSchedulerFor_Logging?.Dispose();   //nah  no need to dispose   the schedulers are reusable
    }

    private void MazeRunnerBenchmarkingUpdatingEventsStream_NextForLogging(object _, IMazeRunnerEventArgs ea)
    {
        var __ = ea switch //@formatter:off
        {
            BenchmarkingCommencingEventArgs               ea_ => ForLogging_OnCommencing_(ea_), //                          benchmarker
            SpecificEngineTestsSuiteStartingEventArgs     ea_ => ForLogging_OnSpecificEngineTestsSuiteStarting_(ea_), //    benchmarker
            
            SpecificEngineSingleLapStartingEventArgs      ea_ => ForLogging_OnSpecificEngineSingleLapStarting_(ea_), //     benchmarker 
            StateChangedEventArgs                         ea_ => ForLogging_OnStateChanged_(ea_), //                        engine
            SpecificEngineSingleLapConcludedEventArgs     ea_ => ForLogging_OnSpecificEngineSingleLapConcluded_(ea_), //    benchmarker
            
            SpecificEngineTestsSuiteCompletedEventArgs    ea_ => ForLogging_OnSpecificEngineTestsSuiteCompleted_(ea_), //   benchmarker
            AllBenchmarkingsDoneEventArgs                 ea_ => ForLogging_OnAllBenchmarkingsDone_(ea_), //                benchmarker
            
            _ => throw new NotImplementedException($"Whoops missing handler for event type: {ea.GetType().Name}") //@formatter:on
        };
        return;

        bool ForLogging_OnAllBenchmarkingsDone_(AllBenchmarkingsDoneEventArgs _)
        {
            Post(PostCallback_);
            return true;

            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($@"{nl}------------ All Done ----------");
            }
        }
        
        bool ForLogging_OnSpecificEngineTestsSuiteCompleted_(SpecificEngineTestsSuiteCompletedEventArgs ea_)
        {
            Post(PostCallback_);
            return true;
            
            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($"{nl2}{ea_.ToStringy(includeShortestPath: true)}{nl}");
            }
        }
        
        bool ForLogging_OnCommencing_(BenchmarkingCommencingEventArgs ea_)
        {
            Post(PostCallback_);
            return true;
            
            void PostCallback_(object _)
            {
                txtLog.Text += $@"{nl2}** Commencing tests on {ea_.Engines.Count} engines with {ea_.LapsPerEngine} laps per engine ... ";
            }
        }
        
        bool ForLogging_OnSpecificEngineSingleLapStarting_(SpecificEngineSingleLapStartingEventArgs ea_)
        {
            Post(PostCallback_);
            return true;

            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($@"{ea_.LapIndex + 1}"); //consider logging on a different subscriber with a separate scheduler
            }
        }

        static bool ForLogging_OnStateChanged_(StateChangedEventArgs eaa)
        {
            //nothing to do logging-wise
            return true;
        }

        bool ForLogging_OnSpecificEngineSingleLapConcluded_(SpecificEngineSingleLapConcludedEventArgs ea_)
        {
            Post(PostCallback_);
            return true;

            void PostCallback_(object _)
            {
                txtLog.AppendTextAndScrollToBottom($@"{ConclusionToSymbol[ea_.Status]}  ");
            }
        }
        
        bool ForLogging_OnSpecificEngineTestsSuiteStarting_(SpecificEngineTestsSuiteStartingEventArgs ea_)
        {
            Post(PostCallback_);
            return true;

            void PostCallback_(object state)
            {
                txtLog.Text += $@"{nl2}**** Commencing tests on Engine '{ea_.Engine.GetEngineName()}'. Completed Laps: ";
            }
        }
    }

    private void MazeRunnerBenchmarkingUpdatingEventsStream_NextForUI(object _, IMazeRunnerEventArgs ea)
    {
        if (_tokenSource.IsCancellationRequested)
        {
            OnBenchmarkingCancelled();
            return;
        }
        
        var __ = ea switch //@formatter:off
        {
            BenchmarkingCommencingEventArgs          ea_ => ForUI_OnCommencing_(ea_), //                      benchmarker
            SpecificEngineTestsSuiteStartingEventArgs     ea_ => ForUI_OnSpecificEngineTestsSuiteStarting_(ea_), //     benchmarker
            
            SpecificEngineSingleLapStartingEventArgs ea_ => ForUI_OnSpecificEngineSingleLapStarting_(ea_), // benchmarker 
            StateChangedEventArgs                    ea_ => ForUI_OnStateChanged_(ea_), //                    engine
            SpecificEngineSingleLapConcludedEventArgs      ea_ => ForUI_OnSpecificEngineSingleLapConcluded_(ea_), //      benchmarker
            LapConcludedEventArgs                ea_ => ForUI_OnAllLapsConcluded_(ea_), //                engine
            
            SpecificEngineTestsSuiteCompletedEventArgs    ea_ => ForUI_specificEngineTestsSuiteCompleted_(ea_), //      benchmarker
            AllBenchmarkingsDoneEventArgs             ea_ => ForUI_OnAllBenchmarkingsDone_(ea_), //             benchmarker
            
            _ => throw new NotImplementedException($"Whoops missing handler for event type: {ea.GetType().Name}") //@formatter:on
        };
        return;

        static bool ForUI_OnAllLapsConcluded_(LapConcludedEventArgs ea_)
        {
            // nothing to do ui-wise
            return true;
        }

        bool ForUI_OnAllBenchmarkingsDone_(AllBenchmarkingsDoneEventArgs _)
        {
            Send(SendCallback_);
            return true;

            void SendCallback_(object _)
            {
                OnComponentStateChanged(new ComponentStateChanged("testbench.alldone"));
            }
        }
        
        static bool ForUI_specificEngineTestsSuiteCompleted_(SpecificEngineTestsSuiteCompletedEventArgs ea_)
        {
            // nothing to do ui-wise
            return true;
        }
        
        bool ForUI_OnCommencing_(BenchmarkingCommencingEventArgs ea_)
        {
            Send(SendCallback_);
            return true;
            
            void SendCallback_(object state)
            {
                OnComponentStateChanged(new ComponentStateChanged("testbench.launching"));
            }
        }
        
        bool ForUI_OnSpecificEngineSingleLapStarting_(SpecificEngineSingleLapStartingEventArgs ea_)
        {
            Post(PostCallback_);
            return true;

            void PostCallback_(object _)
            {
                _ccMazeCanvas.ResetCellsToDefaultColors();
            }
        }
        
        bool ForUI_OnStateChanged_(StateChangedEventArgs eaa)
        {
            if (!_isMazeTooLarge)
            {
                Send(SendCallback_);
            }
            
            if (!_isDelayTooSmall) Thread.Sleep((int)nudMovementDelay.Value); //must be done on the consumption thread to enforce the slow-down

            return true;
                
            void SendCallback_(object _)
            {
                var label1 = (Label) null;
                var label2 = (Label) null;
                try
                {
                    _ccMazeCanvas.tlpMesh.SuspendLayout();
                    _ccMazeCanvas.tlpMesh.SuspendDrawing();

                    if (eaa.NewTip != null) label1 = _ccMazeCanvas.CustomizeCell(eaa.NewTip.Value, NewTipPositionColor, eaa.StepIndex.ToString());
                    if (eaa.OldTip != null) label2 = _ccMazeCanvas.CustomizeCell(eaa.OldTip.Value, eaa.IsProgressNotBacktracking ? TrajectorySquareColor : InvalidatedSquareColor);
                }
                finally
                {
                    _ccMazeCanvas.tlpMesh.ResumeDrawing();
                    _ccMazeCanvas.tlpMesh.ResumeLayout();

                    label1?.Visible = true; //to avoid flickering we set the label to visible only after the layout is resumed
                    label2?.Visible = true; //to avoid flickering we set the label to visible only after the layout is resumed
                }
            }
        }

        bool ForUI_OnSpecificEngineSingleLapConcluded_(SpecificEngineSingleLapConcludedEventArgs ea_)
        {
            // nothing to do ui-wise
            return true;
        }
        
        bool ForUI_OnSpecificEngineTestsSuiteStarting_(SpecificEngineTestsSuiteStartingEventArgs ea_)
        {
            // nothing to do ui-wise
            return true;
        }
    }

    static void MazeRunnerBenchmarkingUpdatesSubjectSubscriber_Errored(Exception ex_, bool _)
    {
        HandlePossibleOops(ex_);
    }

    static private void HandlePossibleOops(Exception ex)
    {
        if (ex is OperationCanceledException)
            return; //ignore control-flow exceptions
        
        using var form = new FormUnhandledException(ex);

        form.ShowDialog();
    }

    private void btnStop_Click(object sender, EventArgs ea)
    {
        _tokenSource.Cancel();
        
        // once a token-source instance gets cancelled it is all over for said instance
        // we thus reinstantiate the token-source inside btnstart_click
    }

    private async void saveMazeToolStripMenuItem_Click(object sender, EventArgs ea)
    {
        var filepath = "";

        try
        {
            using var saveFileConfigurationDialog = new SaveFileDialog();
            
            saveFileConfigurationDialog.Title = @"Save Maze as";
            saveFileConfigurationDialog.Filter = $@"Maze Files (*{MazefileExtension})|*{MazefileExtension}";
            saveFileConfigurationDialog.FileName = $"mazemap_{DateTime.Now:yyyyMMddHHmmss}_{_ccMazeCanvas.Maze.Size.Height:D5}x{_ccMazeCanvas.Maze.Size.Width:D5}{MazefileExtension}";
            saveFileConfigurationDialog.AddExtension = true;
            saveFileConfigurationDialog.ValidateNames = true;
            saveFileConfigurationDialog.CheckPathExists = true;
            saveFileConfigurationDialog.OverwritePrompt = true;
            saveFileConfigurationDialog.InitialDirectory = DesktopDirectory;
            if (saveFileConfigurationDialog.ShowDialog(this) != DialogResult.OK) return;

            filepath = saveFileConfigurationDialog.FileName;
        }
        catch (Exception ex)
        {
            ShowMessageSafe("Failed to show file-save dialog.", "Error", ex: ex);
            return;
        }

        try
        {
            await File.WriteAllTextAsync(filepath, _ccMazeCanvas.Maze.ToAsciiMap());

            using var formFileGeneratedSuccessfully = new FormNotificationAboutFileOperation();
            
            formFileGeneratedSuccessfully.Text = @"Maze Saved Successfully";
            formFileGeneratedSuccessfully.FilePath = filepath;
            formFileGeneratedSuccessfully.FileGeneratedSuccessfullyMessage = @"Operation completed successfully";
            formFileGeneratedSuccessfully.ShowDialog(this);
        }
        catch (Exception ex)
        {
            ShowMessageSafe($"Failed save maze file to:{nl2}{filepath}{nl2}Please select a different location.", @"Error saving to disk", ex: ex);
            return;
        }
    }

    private async void loadMazeToolStripMenuItem_Click(object sender, EventArgs ea)
    {
        var filepath = "";
        try
        {
            using var openFileDialog = new OpenFileDialog();

            openFileDialog.Title = @"Select existing backup file";
            openFileDialog.Filter = $@"Maze Files (*{MazefileExtension})|*{MazefileExtension}|All Files(*.*)|*.*";
            openFileDialog.ValidateNames = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.InitialDirectory = DesktopDirectory;
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            filepath = openFileDialog.FileName;
        }
        catch (Exception ex)
        {
            ShowMessageSafe(ex.Message, @"Mazefile Loading Failed", ex: ex);
            return;
        }
        
        try
        {
            _ccMazeCanvas.Maze = await _mazesFactory.FromFileAsync(filepath);
        }
        catch (Exception ex)
        {
            ShowMessageSafe("Failed to restore backup copy", "Error reading from disk", ex: ex);
            return;
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

        using var generateMazeDialog = new FormGenerateNewRandomMaze();
        
        generateMazeDialog.MazeWidth = mazespecs.Width;
        generateMazeDialog.MazeHeight = mazespecs.Height;
        generateMazeDialog.MazeDensity = mazespecs.RoadblockDensity;
        if (generateMazeDialog.ShowDialog(this) != DialogResult.OK) return;

        _ccMazeCanvas.Maze = _mazesFactory.SpawnRandom(generateMazeDialog.MazeWidth, generateMazeDialog.MazeHeight, generateMazeDialog.MazeDensity);
    }

    private void Post(SendOrPostCallback callback, object data = null) => _syncContext.Post(callback, data);
    private void Send(SendOrPostCallback callback, object data = null) => _syncContext.Send(callback, data);

    protected DialogResult ShowMessageSafe(
        string text,
        string caption,
        MessageBoxButtons buttons = MessageBoxButtons.OK,
        MessageBoxIcon icon = MessageBoxIcon.Error,
        MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
        Exception ex = null,
        IWin32Window owner = null
    ) => MessageBox.Show(
        text: $"""
               {text}

               {ex}
               """,
        icon: icon,
        owner: owner ?? this,
        caption: caption,
        buttons: buttons,
        defaultButton: defaultButton
    );

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