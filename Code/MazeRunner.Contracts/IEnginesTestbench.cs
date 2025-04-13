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
    event EventHandler<SpecificEngineLapStartingEventArgs> SpecificEngineLapStarting;
    event EventHandler<SpecificEngineLapConcludedEventArgs> SpecificEngineLapConcluded;
    event EventHandler<SpecificEngineTestsStartingEventArgs> SpecificEngineTestsStarting;
    event EventHandler<SpecificEngineTestsCompletedEventArgs> SpecificEngineTestsCompleted;

    Task RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
}