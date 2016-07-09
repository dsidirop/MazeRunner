using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared.Engine
{
    public interface IMazeRunnerEngine
    {
        event EventHandler EngineStateChanged; //progressed
        event EventHandler<ConcludedEventArgs> Concluded;

        Point TrajectoryTip { get; }
        IEnumerable<Point> TestedSquares { get; }

        void Run();
    }
}