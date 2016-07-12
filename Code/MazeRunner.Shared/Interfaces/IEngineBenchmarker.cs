using System;
using System.Collections.Generic;

namespace MazeRunner.Shared.Interfaces
{
    public interface IEngineBenchmarker
    {
        event EventHandler AllDone;
        event EventHandler<LapConcludedEventArgs> LapCompleted;
        event EventHandler<SingleEngineTestsCompletedEventArgs> SingleEngineTestsCompleted;

        void Run(IReadOnlyCollection<IMazeRunnerEngine> enginesToTest, int repetitions);
    }
}