using System;
using System.Collections.Generic;
using System.Threading;
using MazeRunner.Shared.Interfaces.Events;

namespace MazeRunner.Shared.Interfaces
{
    public interface IEnginesTestbench
    {
        bool Running { get; }

        event EventHandler<AllDoneEventArgs> AllDone;
        event EventHandler<CommencingEventArgs> Commencing;
        event EventHandler<LapStartingEventArgs> LapStarting;
        event EventHandler<LapConcludedEventArgs> LapConcluded;
        event EventHandler<SingleEngineTestsStartingEventArgs> SingleEngineTestsStarting;
        event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted;

        void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
        void RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
    }
}