using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Contracts;
using MazeRunner.Contracts.Events;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.Utils;

namespace MazeRunner.EnginesFactory.Benchmark;

public class EnginesTestbench : IEnginesTestbench
{
    public readonly TraceSource Tracer = new(nameof(EnginesTestbench), SourceLevels.Off);

    static private int _benchmarkRuns;

    private event EventHandler<AllBenchmarkingsDoneEventArgs> _allBenchmarkingsDone;
    public event EventHandler<AllBenchmarkingsDoneEventArgs> AllBenchmarkingsDone
    {
        add
        {
            _allBenchmarkingsDone -= value;
            _allBenchmarkingsDone += value;
        }
        remove => _allBenchmarkingsDone -= value;
    }

    private event EventHandler<BenchmarkingCommencingEventArgs> _benchmarkingCommencing;
    public event EventHandler<BenchmarkingCommencingEventArgs> BenchmarkingCommencing
    {
        add
        {
            _benchmarkingCommencing -= value;
            _benchmarkingCommencing += value;
        }
        remove => _benchmarkingCommencing -= value;
    }

    private event EventHandler<SpecificEngineSingleLapStartingEventArgs> _specificEngineSingleLapStarting;
    public event EventHandler<SpecificEngineSingleLapStartingEventArgs> SpecificEngineSingleLapStarting
    {
        add
        {
            _specificEngineSingleLapStarting -= value;
            _specificEngineSingleLapStarting += value;
        }
        remove => _specificEngineSingleLapStarting -= value;
    }

    private event EventHandler<SpecificEngineSingleLapConcludedEventArgs> _SpecificEngineSingleLapConcluded;
    public event EventHandler<SpecificEngineSingleLapConcludedEventArgs> SpecificEngineSingleLapConcluded
    {
        add
        {
            _SpecificEngineSingleLapConcluded -= value;
            _SpecificEngineSingleLapConcluded += value;
        }
        remove => _SpecificEngineSingleLapConcluded -= value;
    }

    private event EventHandler<SpecificEngineTestsSuiteStartingEventArgs> _SpecificEngineTestsSuiteStarting;
    public event EventHandler<SpecificEngineTestsSuiteStartingEventArgs> SpecificEngineTestsSuiteStarting
    {
        add
        {
            _SpecificEngineTestsSuiteStarting -= value;
            _SpecificEngineTestsSuiteStarting += value;
        }
        remove => _SpecificEngineTestsSuiteStarting -= value;
    }

    private event EventHandler<SpecificEngineTestsSuiteCompletedEventArgs> _SpecificEngineTestsSuiteCompleted;
    public event EventHandler<SpecificEngineTestsSuiteCompletedEventArgs> SpecificEngineTestsSuiteCompleted
    {
        add
        {
            _SpecificEngineTestsSuiteCompleted -= value;
            _SpecificEngineTestsSuiteCompleted += value;
        }
        remove => _SpecificEngineTestsSuiteCompleted -= value;
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

        var stopWatch = new Stopwatch();
        var currentLap = 0;
        var benchmarkId = Interlocked.Increment(ref _benchmarkRuns); //i++ is not threadsafe
        var failedEngine = (IMazeRunnerEngine) null;
        try
        {
            OnCommencing(new BenchmarkingCommencingEventArgs(
                engines: enginesToTest,
                benchmarkId: benchmarkId,
                lapsPerEngine: repetitions
            ));

            foreach (var eng in enginesToTest)
            {
                failedEngine = eng;
                OnSpecificEngineTestsSuiteStarting(new SpecificEngineTestsSuiteStartingEventArgs(benchmarkId, eng));

                var crashes = 0;
                var pathLengths = new List<int>(repetitions);
                var shortestPath = (IReadOnlyCollection<Point>) null;
                var timeDurations = new List<TimeSpan>(repetitions);

                var ii = 0;
                try
                {
                    eng.Starting += Engine_Starting_;
                    eng.Concluded += Engine_Concluded_;
                    for (var i = 0; i < repetitions; i++, eng.Reset())
                    {
                        ct.ThrowIfCancellationRequested();
                        
                        currentLap = i;
                        eng.Run(cancellationToken); //safe
                    }
                }
                finally
                {
                    eng.Starting -= Engine_Starting_;
                    eng.Concluded -= Engine_Concluded_;
                }

                pathLengths.Sort();
                timeDurations.Sort();
                    
                OnSpecificEngineTestsSuiteCompleted(new SpecificEngineTestsSuiteCompletedEventArgs
                (
                    engine: eng,
                    crashes: crashes,
                    benchmarkId: benchmarkId,
                    repetitions: repetitions,
#pragma warning disable CA1508
                    shortestPath: shortestPath ?? [],
#pragma warning restore CA1508
                    bestPathLength: pathLengths.First(),
                    worstPathLength: pathLengths.Last(),
                    averagePathLength: pathLengths.Average(),
                    bestTimePerformance: timeDurations.First(),
                    worstTimePerformance: timeDurations.Last(),
                    averageTimePerformance: new TimeSpan((long) timeDurations.Average(timeSpan => timeSpan.Ticks))
                ));
                continue;

                void Engine_Starting_(object _, EventArgs __)
                {
                    stopWatch.Restart();
                    OnLapStarting(new SpecificEngineSingleLapStartingEventArgs(benchmarkId, lapIndex: ii, eng));
                }
                
                void Engine_Concluded_(object _, AllLapsConcludedEventArgs ea_)
                {
                    try
                    {
                        stopWatch.Stop(); //order
                        if (ea_.Status is ConclusionStatusTypeEnum.Stopped or ConclusionStatusTypeEnum.Crashed)
                        {
                            if (ea_.Status == ConclusionStatusTypeEnum.Crashed) crashes++;
                            return;
                        }

                        timeDurations.Add(stopWatch.Elapsed); //order
                        pathLengths.Add(eng.TrajectoryLength); //order
                    }
                    finally
                    {
                        shortestPath = (shortestPath?.Count ?? int.MaxValue) > eng.TrajectoryLength
                            ? eng.Trajectory.ToArray().AsReadOnly() //0 tolist
                            : shortestPath;

                        OnLapConcluded(new SpecificEngineSingleLapConcludedEventArgs( //order
                            engine: eng,
                            status: ea_.Status,
                            lapIndex: ii++,
                            duration: stopWatch.Elapsed,
                            benchmarkId: benchmarkId
                        ));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            OnException(benchmarkId, failedEngine, currentLap, ex);

            throw;
        }
        finally
        {
            OnAllDone(new AllBenchmarkingsDoneEventArgs(benchmarkId));
        }

        //00  it is crucial to snapshot the best-path by means of tolist because the engine state gets reset from one lap to the next and with it the trajectory
        //    property gets wiped clean
    }

    protected virtual void OnAllDone(in AllBenchmarkingsDoneEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] All benchmarks done");

        Running = false;
        _allBenchmarkingsDone?.Invoke(this, ea);
    }

    protected virtual void OnCommencing(in BenchmarkingCommencingEventArgs ea)
    {
        Tracer.TraceInformation($"""
                                 [#{ea!.BenchmarkId}] Commencing benchmarks on the following engines [{ea!.LapsPerEngine} lap(s) per engine]:

                                 {ea!.Engines!.Select(x => x.GetEngineName()).LineJoinify()}
                                 """);

        Running = true;
        _benchmarkingCommencing?.Invoke(this, ea);
    }

    protected virtual void OnLapStarting(in SpecificEngineSingleLapStartingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Starting lap#{ea!.LapIndex} for engine '{ea!.Engine!.GetEngineName()}'");

        _specificEngineSingleLapStarting?.Invoke(this, ea);
    }

    protected virtual void OnLapConcluded(in SpecificEngineSingleLapConcludedEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Concluded lap#{ea!.LapIndex} for engine '{ea!.Engine!.GetEngineName()}' with status: {ea!.Status} ({ea!.Duration!.TotalMilliseconds}ms)");

        _SpecificEngineSingleLapConcluded?.Invoke(this, ea);
    }

    protected virtual void OnSpecificEngineTestsSuiteStarting(in SpecificEngineTestsSuiteStartingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Benchmarking engine '{ea!.Engine!.GetEngineName()}'");

        _SpecificEngineTestsSuiteStarting?.Invoke(this, ea);
    }

    protected virtual void OnSpecificEngineTestsSuiteCompleted(in SpecificEngineTestsSuiteCompletedEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] All laps completed for engine '{ea!.Engine!.GetEngineName()}':\n\n{ea!.ToStringy(includeShortestPath: false)}");

        _SpecificEngineTestsSuiteCompleted?.Invoke(this, ea);
    }

    // ReSharper disable once UnusedMethodReturnValue.Local    Unused_Method_Return_Value
    private bool OnException(int benchmarkId, IMazeRunnerEngine failedEngine, int currentLap, Exception ex)
    {
        if (ex is OperationCanceledException)
            return false; //ctrl+c or stop button    we dont want to log this

        Tracer.TraceEvent(
            id: 0,
            eventType: TraceEventType.Error,
            message: $"""
                      [#{benchmarkId}] Benchmark crashed:

                      Lap# {currentLap}
                      Engine being benchmarked: {failedEngine.GetEngineName()}

                      {ex}
                      """
        );
        
        return true;
    }
}
