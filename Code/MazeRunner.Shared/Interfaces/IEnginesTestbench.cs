using System;
using System.Collections.Generic;

namespace MazeRunner.Shared.Interfaces
{
    public interface IEnginesTestbench
    {
        event EventHandler AllDone;
        event EventHandler<LapConcludedEventArgs> LapCompleted;
        event EventHandler<SingleEngineTestsStartingEventArgs> SingleEngineTestsStarting;
        event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted;

        void Stop();
        void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions);
    }
}