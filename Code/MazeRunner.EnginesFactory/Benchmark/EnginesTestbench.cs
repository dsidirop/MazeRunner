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

    private event EventHandler<SpecificEngineLapStartingEventArgs> _specificEngineLapStarting;
    public event EventHandler<SpecificEngineLapStartingEventArgs> SpecificEngineLapStarting
    {
        add
        {
            _specificEngineLapStarting -= value;
            _specificEngineLapStarting += value;
        }
        remove => _specificEngineLapStarting -= value;
    }

    private event EventHandler<SpecificEngineLapConcludedEventArgs> _specificEngineLapConcluded;
    public event EventHandler<SpecificEngineLapConcludedEventArgs> SpecificEngineLapConcluded
    {
        add
        {
            _specificEngineLapConcluded -= value;
            _specificEngineLapConcluded += value;
        }
        remove => _specificEngineLapConcluded -= value;
    }

    private event EventHandler<SpecificEngineTestsStartingEventArgs> _specificEngineTestsStarting;
    public event EventHandler<SpecificEngineTestsStartingEventArgs> SpecificEngineTestsStarting
    {
        add
        {
            _specificEngineTestsStarting -= value;
            _specificEngineTestsStarting += value;
        }
        remove => _specificEngineTestsStarting -= value;
    }

    private event EventHandler<SpecificEngineTestsCompletedEventArgs> _specificEngineTestsCompleted;
    public event EventHandler<SpecificEngineTestsCompletedEventArgs> SpecificEngineTestsCompleted
    {
        add
        {
            _specificEngineTestsCompleted -= value;
            _specificEngineTestsCompleted += value;
        }
        remove => _specificEngineTestsCompleted -= value;
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
            OnCommencing(new CommencingEventArgs(
                engines: enginesToTest,
                benchmarkId: benchmarkId,
                lapsPerEngine: repetitions
            ));

            foreach (var eng in enginesToTest)
            {
                failedEngine = eng;
                OnSpecificEngineTestsStarting(new SpecificEngineTestsStartingEventArgs(benchmarkId, eng));

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
                    
                OnSpecificEngineTestsCompleted(new SpecificEngineTestsCompletedEventArgs
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
                    OnLapStarting(new SpecificEngineLapStartingEventArgs(benchmarkId, lapIndex: ii, eng));
                }
                
                void Engine_Concluded_(object _, ConcludedEventArgs ea_)
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

                        OnLapConcluded(new SpecificEngineLapConcludedEventArgs( //order
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
            OnAllDone(new AllDoneEventArgs(benchmarkId));
        }

        //00  it is crucial to snapshot the best-path by means of tolist because the engine state gets reset from one lap to the next and with it the trajectory
        //    property gets wiped clean
    }

    protected virtual void OnAllDone(in AllDoneEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] All benchmarks done");

        Running = false;
        _allDone?.Invoke(this, ea);
    }

    protected virtual void OnCommencing(in CommencingEventArgs ea)
    {
        Tracer.TraceInformation($"""
                                 [#{ea!.BenchmarkId}] Commencing benchmarks on the following engines [{ea!.LapsPerEngine} lap(s) per engine]:

                                 {ea!.Engines!.Select(x => x.GetEngineName()).LineJoinify()}
                                 """);

        Running = true;
        _commencing?.Invoke(this, ea);
    }

    protected virtual void OnLapStarting(in SpecificEngineLapStartingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Starting lap#{ea!.LapIndex} for engine '{ea!.Engine!.GetEngineName()}'");

        _specificEngineLapStarting?.Invoke(this, ea);
    }

    protected virtual void OnLapConcluded(in SpecificEngineLapConcludedEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Concluded lap#{ea!.LapIndex} for engine '{ea!.Engine!.GetEngineName()}' with status: {ea!.Status} ({ea!.Duration!.TotalMilliseconds}ms)");

        _specificEngineLapConcluded?.Invoke(this, ea);
    }

    protected virtual void OnSpecificEngineTestsStarting(in SpecificEngineTestsStartingEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] Benchmarking engine '{ea!.Engine!.GetEngineName()}'");

        _specificEngineTestsStarting?.Invoke(this, ea);
    }

    protected virtual void OnSpecificEngineTestsCompleted(in SpecificEngineTestsCompletedEventArgs ea)
    {
        Tracer.TraceInformation($"[#{ea!.BenchmarkId}] All laps completed for engine '{ea!.Engine!.GetEngineName()}':\n\n{ea!.ToStringy(includeShortestPath: false)}");

        _specificEngineTestsCompleted?.Invoke(this, ea);
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
