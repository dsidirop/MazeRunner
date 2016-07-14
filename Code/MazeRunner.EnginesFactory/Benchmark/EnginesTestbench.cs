using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;

namespace MazeRunner.EnginesFactory.Benchmark
{
    public class EnginesTestbench : IEnginesTestbench
    {
        private event EventHandler<LapConcludedEventArgs> _lapConcluded;
        public event EventHandler<LapConcludedEventArgs> LapCompleted
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

        private event EventHandler _allDone;
        public event EventHandler AllDone
        {
            add
            {
                _allDone -= value;
                _allDone += value;
            }
            remove { _allDone -= value; }
        }

        public void RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null)
            => Task.Factory.StartNew(() => Run(enginesToTest, repetitions, cancellationToken), cancellationToken ?? CancellationToken.None);

        public void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null) //0 ireadonlycollection https://msdn.microsoft.com/en-us/library/hh881542
        {
            if (repetitions <= 0) throw new ArgumentOutOfRangeException(nameof(repetitions));
            if (enginesToTest?.Any(x => x == null) ?? true) throw new ArgumentNullException(nameof(enginesToTest));

            cancellationToken = cancellationToken ?? CancellationToken.None;
            try
            {
                var stopWatch = new Stopwatch();
                enginesToTest.ForEach(eng =>
                {
                    OnSingleEngineTestsStarting(new SingleEngineTestsStartingEventArgs {Engine = eng});

                    var crashes = 0;
                    var pathlengths = new List<int>(repetitions);
                    var timedurations = new List<TimeSpan>(repetitions);

                    var ii = 0;
                    var starting = new EventHandler((s, ea) => stopWatch.Restart());
                    var concluded = new EventHandler<ConcludedEventArgs>((s, ea) =>
                    {
                        stopWatch.Stop(); //order
                        if (ea.Crashed)
                        {
                            crashes++;
                            return;
                        }

                        timedurations.Add(stopWatch.Elapsed); //order
                        pathlengths.Add(eng.TrajectoryLength); //order
                        OnLapConcluded(new LapConcludedEventArgs {LapIndex = ii++, Duration = stopWatch.Elapsed, Engine = eng}); //order
                    });

                    try
                    {
                        eng.Starting += starting;
                        eng.Concluded += concluded;
                        for (var i = 0; i < repetitions; i++, eng.Reset())
                        {
                            eng.Run(); //safe
                        }
                    }
                    finally
                    {
                        eng.Starting -= starting;
                        eng.Concluded -= concluded;
                    }

                    pathlengths.Sort();
                    timedurations.Sort();
                    OnSingleEngineTestsCompleted(new SingleEngineTestsCompletedEventArgs
                    {
                        Engine = eng,
                        Crashes = crashes,
                        Repetitions = repetitions,
                        BestPathLength = pathlengths.First(),
                        WorstPathLength = pathlengths.Last(),
                        AveragePathLength = pathlengths.Average(),
                        BestTimePerformance = timedurations.First(),
                        WorstTimePerformance = timedurations.Last(),
                        AverageTimePerformance = new TimeSpan(Convert.ToInt64(timedurations.Average(timeSpan => timeSpan.Ticks)))
                    });
                });
            }
            finally
            {
                OnAllDone();
            }
        }


        protected virtual void OnAllDone() => _allDone?.Invoke(this, EventArgs.Empty);
        protected virtual void OnLapConcluded(LapConcludedEventArgs ea) => _lapConcluded?.Invoke(this, ea);
        protected virtual void OnSingleEngineTestsStarting(SingleEngineTestsStartingEventArgs ea) => _singleEngineTestsStarting?.Invoke(this, ea);
        protected virtual void OnSingleEngineTestsCompleted(SingleEngineTestsCompletedEventArgs ea) => _singleEngineTestsCompleted?.Invoke(this, ea);
        
    }
}
