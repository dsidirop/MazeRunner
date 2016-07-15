using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Shared;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
using MazeRunner.Shared.Interfaces.Events;

namespace MazeRunner.EnginesFactory.Benchmark
{
    public class EnginesTestbench : IEnginesTestbench
    {
        public readonly TraceSource Tracer = new TraceSource(nameof(EnginesTestbench), SourceLevels.Off);

        static private int _benchmarkRuns;

        private event EventHandler<AllDoneEventArgs> _allDone;
        public event EventHandler<AllDoneEventArgs> AllDone
        {
            add
            {
                _allDone -= value;
                _allDone += value;
            }
            remove { _allDone -= value; }
        }


        private event EventHandler<CommencingEventArgs> _commencing;
        public event EventHandler<CommencingEventArgs> Commencing
        {
            add
            {
                _commencing -= value;
                _commencing += value;
            }
            remove { _commencing -= value; }
        }

        private event EventHandler<LapStartingEventArgs> _lapStarting;
        public event EventHandler<LapStartingEventArgs> LapStarting
        {
            add
            {
                _lapStarting -= value;
                _lapStarting += value;
            }
            remove { _lapStarting -= value; }
        }

        private event EventHandler<LapConcludedEventArgs> _lapConcluded;
        public event EventHandler<LapConcludedEventArgs> LapConcluded
        {
            add
            {
                _lapConcluded -= value;
                _lapConcluded += value;
            }
            remove { _lapConcluded -= value; }
        }

        private event EventHandler<SingleEngineTestsStartingEventArgs> _singleEngineTestsStarting;
        public event EventHandler<SingleEngineTestsStartingEventArgs> SingleEngineTestsStarting
        {
            add
            {
                _singleEngineTestsStarting -= value;
                _singleEngineTestsStarting += value;
            }
            remove { _singleEngineTestsStarting -= value; }
        }

        private event EventHandler<SingleEngineTestsCompletedEventArgs> _singleEngineTestsCompleted;
        public event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted
        {
            add
            {
                _singleEngineTestsCompleted -= value;
                _singleEngineTestsCompleted += value;
            }
            remove { _singleEngineTestsCompleted -= value; }
        }

        public bool Running { get; private set; }

        public void RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null)
            => Task.Factory.StartNew(() => Run(enginesToTest, repetitions, cancellationToken), cancellationToken ?? CancellationToken.None);

        public void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null) //0 ireadonlycollection https://msdn.microsoft.com/en-us/library/hh881542
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
                    var pathlengths = new List<int>(repetitions);
                    var shortestpath = (IList<Point>) null;
                    var timedurations = new List<TimeSpan>(repetitions);

                    var ii = 0;
                    var lapstarting = new EventHandler((s, ea) =>
                    {
                        stopWatch.Restart();
                        OnLapStarting(new LapStartingEventArgs { BenchmarkId = benchmarkId, LapIndex = ii, Engine = eng});
                    });
                    var lapconcluded = new EventHandler<ConcludedEventArgs>((s, ea) =>
                    {
                        try
                        {
                            stopWatch.Stop(); //order
                            if (ea.Status == ConclusionStatusTypeEnum.Cancelled || ea.Status == ConclusionStatusTypeEnum.Crashed)
                            {
                                if (ea.Status == ConclusionStatusTypeEnum.Crashed) crashes++;
                                return;
                            }
                            timedurations.Add(stopWatch.Elapsed); //order
                            pathlengths.Add(eng.TrajectoryLength); //order
                        }
                        finally
                        {
                            shortestpath = (shortestpath?.Count ?? int.MaxValue) > eng.TrajectoryLength ? eng.Trajectory.ToList() : shortestpath; //0 tolist
                            OnLapConcluded(new LapConcludedEventArgs { BenchmarkId = benchmarkId, Status = ea.Status, LapIndex = ii++, Duration = stopWatch.Elapsed, Engine = eng}); //order
                        }
                    });

                    try
                    {
                        eng.Starting += lapstarting;
                        eng.Concluded += lapconcluded;
                        for (var i = 0; i < repetitions; i++, eng.Reset())
                        {
                            currentLap = i;
                            eng.Run(cancellationToken); //safe
                            ct.ThrowIfCancellationRequested();
                        }
                    }
                    finally
                    {
                        eng.Starting -= lapstarting;
                        eng.Concluded -= lapconcluded;
                    }

                    pathlengths.Sort();
                    timedurations.Sort();
                    OnSingleEngineTestsCompleted(new SingleEngineTestsCompletedEventArgs
                    {
                        Engine = eng,
                        Crashes = crashes,
                        BenchmarkId = benchmarkId,
                        Repetitions = repetitions,
                        ShortestPath = shortestpath ?? new List<Point>(0),
                        BestPathLength = pathlengths.First(),
                        WorstPathLength = pathlengths.Last(),
                        AveragePathLength = pathlengths.Average(),
                        BestTimePerformance = timedurations.First(),
                        WorstTimePerformance = timedurations.Last(),
                        AverageTimePerformance = new TimeSpan(Convert.ToInt64(timedurations.Average(timeSpan => timeSpan.Ticks)))
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
        }
        //0 its crucial to snapshot the bestpath by means of tolist because the engine state gets reseted from one lap to the next and with it the trajectory
        //  property gets wiped clean

        protected virtual void OnAllDone(AllDoneEventArgs ea)
        {
            Tracer.TraceInformation($"[#{ea.BenchmarkId}] All benchmarks done");

            Running = false;
            _allDone?.Invoke(this, ea);
        }

        protected virtual void OnCommencing(CommencingEventArgs ea)
        {
            Tracer.TraceInformation($"[#{ea.BenchmarkId}] Commencing benchmarks on the following engines [{ea.RepetitionsPerEngine} lap(s) per engine]:{U.nl2}{string.Join(U.nl, ea.Engines.Select(x => x.GetEngineName()))}");

            Running = true;
            _commencing?.Invoke(this, ea);
        }

        protected virtual void OnLapStarting(LapStartingEventArgs ea)
        {
            Tracer.TraceInformation($"[#{ea.BenchmarkId}] Starting lap#{ea.LapIndex} for engine '{ea.Engine.GetEngineName()}'");

            _lapStarting?.Invoke(this, ea);
        }

        protected virtual void OnLapConcluded(LapConcludedEventArgs ea)
        {
            Tracer.TraceInformation($"[#{ea.BenchmarkId}] Concluded lap#{ea.LapIndex} for engine '{ea.Engine.GetEngineName()}' with status: {ea.Status} ({ea.Duration.TotalMilliseconds}ms)");

            _lapConcluded?.Invoke(this, ea);
        }

        protected virtual void OnSingleEngineTestsStarting(SingleEngineTestsStartingEventArgs ea)
        {
            Tracer.TraceInformation($"[#{ea.BenchmarkId}] Benchmarking engine '{ea.Engine.GetEngineName()}'");

            _singleEngineTestsStarting?.Invoke(this, ea);
        }

        protected virtual void OnSingleEngineTestsCompleted(SingleEngineTestsCompletedEventArgs ea)
        {
            Tracer.TraceInformation(ea.ToString(includeShortestPath: false));

            _singleEngineTestsCompleted?.Invoke(this, ea);
        }

        private void OnException(int benchmarkId, IMazeRunnerEngine failedEngine, int currentLap, Exception ex)
        {
            if (ex is OperationCanceledException) return; //stop button

            Tracer.TraceEvent(TraceEventType.Error, 0,
                $"[#{benchmarkId}] Benchmark crashed:{U.nl2}" +
                $"Lap# {currentLap}{U.nl}" +
                $"Engine being benchmarked: {failedEngine.GetEngineName()}{U.nl}" +
                $"{ex}");
        }
    }
}
