using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Contracts.Events;

namespace MazeRunner.Contracts;

public interface IEnginesTestbench
{
    bool Running { get; }

    event EventHandler<AllDoneEventArgs> AllDone;
    event EventHandler<CommencingEventArgs> Commencing;
    event EventHandler<LapStartingEventArgs> LapStarting;
    event EventHandler<LapConcludedEventArgs> LapConcluded;
    event EventHandler<SingleEngineTestsStartingEventArgs> SingleEngineTestsStarting;
    event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted;

    Task RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
}