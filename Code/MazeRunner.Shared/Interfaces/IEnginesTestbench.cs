using System;
using System.Collections.Generic;
using System.Threading;

namespace MazeRunner.Shared.Interfaces
{
    public interface IEnginesTestbench
    {
        event EventHandler AllDone;
        event EventHandler<LapConcludedEventArgs> LapCompleted;
        event EventHandler<SingleEngineTestsStartingEventArgs> SingleEngineTestsStarting;
        event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted;

        void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
        void RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
    }
}