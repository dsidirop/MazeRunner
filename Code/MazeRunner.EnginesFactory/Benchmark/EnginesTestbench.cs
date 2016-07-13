using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions) //0 ireadonlycollection https://msdn.microsoft.com/en-us/library/hh881542
        {
            if (repetitions <= 0) throw new ArgumentOutOfRangeException(nameof(repetitions));
            if (enginesToTest?.Any(x => x == null) ?? true) throw new ArgumentNullException(nameof(enginesToTest));

            try
            {
                var stopWatch = new Stopwatch();
                enginesToTest.ForEach(eng =>
                {
                    var crashes = 0;
                    var pathlengths = new List<int>(repetitions);
                    var timedurations = new List<TimeSpan>(repetitions);

                    var ii = 0;
                    eng.Concluded += (s, ea) =>
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
                    };
                    eng.Starting += (s, ea) => stopWatch.Restart();

                    for (var i = 0; i < repetitions; i++, eng.Reset())
                    {
                        eng.Run(); //safe
                    }

                    pathlengths.Sort();
                    timedurations.Sort();
                    OnSingleEngineTestsCompleted(new SingleEngineTestsCompletedEventArgs
                    {
                        Engine = eng,
                        Crashes = crashes,
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
                OnDone();
            }
        }

        protected virtual void OnDone() => _allDone?.Invoke(this, EventArgs.Empty);
        protected virtual void OnLapConcluded(LapConcludedEventArgs ea) => _lapConcluded?.Invoke(this, ea);
        protected virtual void OnSingleEngineTestsCompleted(SingleEngineTestsCompletedEventArgs ea) => _singleEngineTestsCompleted?.Invoke(this, ea);
    }
}
