using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Engines.Contracts;
using MazeRunner.EnginesFactory.Contracts.Events;

namespace MazeRunner.EnginesFactory.Contracts;

public interface IEnginesTestbench
{
    bool Running { get; }

    event EventHandler<AllBenchmarkingsDoneEventArgs> AllBenchmarkingsDone;
    event EventHandler<BenchmarkingCommencingEventArgs> BenchmarkingCommencing;
    event EventHandler<SpecificEngineSingleLapStartingEventArgs> SpecificEngineSingleLapStarting;
    event EventHandler<SpecificEngineSingleLapConcludedEventArgs> SpecificEngineSingleLapConcluded;
    event EventHandler<SpecificEngineTestsSuiteStartingEventArgs> SpecificEngineTestsSuiteStarting;
    event EventHandler<SpecificEngineTestsSuiteCompletedEventArgs> SpecificEngineTestsSuiteCompleted;

    Task RunAsync(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions, CancellationToken? cancellationToken = null);
}
