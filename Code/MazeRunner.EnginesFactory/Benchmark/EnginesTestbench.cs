using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Contracts;
using MazeRunner.Contracts.Events;
using MazeRunner.Utils;

namespace MazeRunner.EnginesFactory.Benchmark;

public class EnginesTestbench : IEnginesTestbench
{
    public readonly TraceSource Tracer = new(nameof(EnginesTestbench), SourceLevels.Off);

    static private int _benchmarkRuns;

    private event EventHandler<AllDoneEventArgs> _allDone;
    public event EventHandler<AllDoneEventArgs> AllDone
    {
        add
        {
            _allDone -= value;
            _allDone += value;
        }
        remove => _allDone -= value;
    }


    private event EventHandler<CommencingEventArgs> _commencing;
    public event EventHandler<CommencingEventArgs> Commencing
    {
        add
        {
            _commencing -= value;
            _commencing += value;
        }
        remove => _commencing -= value;
    }

    private event EventHandler<LapStartingEventArgs> _lapStarting;
    public event EventHandler<LapStartingEventArgs> LapStarting
    {
        add
        {
            _lapStarting -= value;
            _lapStarting += value;
        }
        remove => _lapStarting -= value;
    }

    private event EventHandler<LapConcludedEventArgs> _lapConcluded;
    public event EventHandler<LapConcludedEventArgs> LapConcluded
    {
        add
        {
            _lapConcluded -= value;
            _lapConcluded += value;
        }
        remove => _lapConcluded -= value;
    }

    private event EventHandler<SingleEngineTestsStartingEventArgs> _singleEngineTestsStarting;
    public event EventHandler<SingleEngineTestsStartingEventArgs> SingleEngineTestsStarting
    {
        add
        {
            _singleEngineTestsStarting -= value;
            _singleEngineTestsStarting += value;
        }
        remove => _singleEngineTestsStarting -= value;
    }

    private event EventHandler<SingleEngineTestsCompletedEventArgs> _singleEngineTestsCompleted;
    public event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted
    {
        add
        {
            _singleEngineTestsCompleted -= value;
            _singleEngineTestsCompleted += value;
        }
        remove => _singleEngineTestsCompleted -= value;
    }

    public bool Running { get; private set; }

    public async Task RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null)
    {
        cancellationToken ??= CancellationToken.None;
        
        await Task.Run( //00 vital
            action: () => Run(enginesToTest, repetitions, cancellationToken),
            cancellationToken: cancellationToken.Value
        );
        
        //00  best to run this on a background task to ensure that we dont overload the UI thread
    }

    private void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null) //0 ireadonlycollection https://msdn.microsoft.com/en-us/library/hh881542
    {
        if (repetitions <= 0) throw new ArgumentOutOfRangeException(nameof(repetitions));
        if (enginesToTest?.Any(x => x == null) ?? true) throw new ArgumentNullException(nameof(enginesToTest));

        var ct = cancellationToken ?? CancellationToken.None;

        var currentLap = 0;
        var benchmarkId = Interlocked.Increment(ref _benchmarkRuns); //i++ is not threadsafe
        var failedEngine = (IMazeRunnerEngine) null;
        try
        {
            OnCommencing(new CommencingEventArgs {BenchmarkId = benchmarkId, Engines = enginesToTest, RepetitionsPerEngine = repetitions});

            var stopWatch = new Stopwatch();
            enginesToTest.ForEach(eng =>
            {
                failedEngine = eng;
                OnSingleEngineTestsStarting(new SingleEngineTestsStartingEventArgs {BenchmarkId = benchmarkId, Engine = eng});

                var crashes = 0;
                var pathLengths = new List<int>(repetitions);
                var shortestPath = (IList<Point>) null;
                var timeDurations = new List<TimeSpan>(repetitions);

                var ii = 0;
                var lapStarting = new EventHandler((_, _) =>
                {
                    stopWatch.Restart();
                    OnLapStarting(new LapStartingEventArgs {BenchmarkId = benchmarkId, LapIndex = ii, Engine = eng});
                });
                var lapConcluded = new EventHandler<ConcludedEventArgs>((_, ea) =>
                {
                    try
                    {
                        stopWatch.Stop(); //order
                        if (ea.Status == ConclusionStatusTypeEnum.Stopped || ea.Status == ConclusionStatusTypeEnum.Crashed)
                        {
                            if (ea.Status == ConclusionStatusTypeEnum.Crashed) crashes++;
                            return;
                        }
                        timeDurations.Add(stopWatch.Elapsed); //order
                        pathLengths.Add(eng.TrajectoryLength); //order
                    }
                    finally
                    {
                        shortestPath = (shortestPath?.Count ?? int.MaxValue) > eng.TrajectoryLength ? eng.Trajectory.ToList() : shortestPath; //0 tolist
                        OnLapConcluded(new LapConcludedEventArgs {BenchmarkId = benchmarkId, Status = ea.Status, LapIndex = ii++, Duration = stopWatch.Elapsed, Engine = eng}); //order
                    }
                });

                try
                {
                    eng.Starting += lapStarting;
                    eng.Concluded += lapConcluded;
                    for (var i = 0; i < repetitions; i++, eng.Reset())
                    {
                        ct.ThrowIfCancellationRequested();
                        
                        currentLap = i;
                        eng.Run(cancellationToken); //safe
                    }
                }
                finally
                {
                    eng.Starting -= lapStarting;
                    eng.Concluded -= lapConcluded;
                }

                pathLengths.Sort();
                timeDurations.Sort();
                OnSingleEngineTestsCompleted(new SingleEngineTestsCompletedEventArgs
                {
                    Engine = eng,
                    Crashes = crashes,
                    BenchmarkId = benchmarkId,
                    Repetitions = repetitions,
                    ShortestPath = shortestPath ?? [],
                    BestPathLength = pathLengths.First(),
                    WorstPathLength = pathLengths.Last(),
                    AveragePathLength = pathLengths.Average(),
                    BestTimePerformance = timeDurations.First(),
                    WorstTimePerformance = timeDurations.Last(),
                    AverageTimePerformance = new TimeSpan(Convert.ToInt64(timeDurations.Average(timeSpan => timeSpan.Ticks)))
                });
            });
        }
        catch (Exception ex)
        {
            OnException(benchmarkId, failedEngine, currentLap, ex);

            throw;
        }
        finally
        {
            OnAllDone(new AllDoneEventArgs {BenchmarkId = benchmarkId});
        }
        
        //00  it is crucial to snapshot the best-path by means of tolist because the engine state gets reset from one lap to the next and with it the trajectory
        //    property gets wiped clean
    }

    protected virtual void OnAllDone(AllDoneEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] All benchmarks done");

        Running = false;
        _allDone?.Invoke(this, ea);
    }

    protected virtual void OnCommencing(CommencingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Commencing benchmarks on the following engines [{ea!.RepetitionsPerEngine} lap(s) per engine]:{U.nl2}{string.Join(U.nl, ea!.Engines!.Select(x => x.GetEngineName()))}");

        Running = true;
        _commencing?.Invoke(this, ea);
    }

    protected virtual void OnLapStarting(LapStartingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Starting lap#{ea!.LapIndex} for engine '{ea!.Engine!.GetEngineName()}'");

        _lapStarting?.Invoke(this, ea);
    }

    protected virtual void OnLapConcluded(LapConcludedEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Concluded lap#{ea!.LapIndex} for engine '{ea!.Engine!.GetEngineName()}' with status: {ea!.Status} ({ea!.Duration!.TotalMilliseconds}ms)");

        _lapConcluded?.Invoke(this, ea);
    }

    protected virtual void OnSingleEngineTestsStarting(SingleEngineTestsStartingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Benchmarking engine '{ea!.Engine!.GetEngineName()}'");

        _singleEngineTestsStarting?.Invoke(this, ea);
    }

    protected virtual void OnSingleEngineTestsCompleted(SingleEngineTestsCompletedEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] All laps completed for engine '{ea!.Engine!.GetEngineName()}':{U.nl2}{ea!.ToStringy(includeShortestPath: false)}");

        _singleEngineTestsCompleted?.Invoke(this, ea);
    }

    // ReSharper disable once UnusedMethodReturnValue.Local    Unused_Method_Return_Value
    private bool OnException(int benchmarkId, IMazeRunnerEngine failedEngine, int currentLap, Exception ex)
    {
        if (ex is OperationCanceledException)
            return false; //ctrl+c or stop button    we dont want to log this

        Tracer.TraceEvent(
            id: 0,
            eventType: TraceEventType.Error,
            message: $"[#{benchmarkId}] Benchmark crashed:{U.nl2}" +
                     $"Lap# {currentLap}{U.nl}" +
                     $"Engine being benchmarked: {failedEngine.GetEngineName()}{U.nl}" +
                     $"{ex}"
        );
        
        return true;
    }
}