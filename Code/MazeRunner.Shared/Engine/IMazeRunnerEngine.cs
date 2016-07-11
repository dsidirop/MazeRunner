using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared.Engine
{
    public interface IMazeRunnerEngine
    {
        event EventHandler StateChanged; //progressed
        event EventHandler<ConcludedEventArgs> Concluded;

        int TrajectoryLength { get; }
        Point? TrajectoryTip { get; }
        IEnumerable<Point> Trajectory { get; }
        IEnumerable<Point> InvalidatedSquares { get; }

        IMazeRunnerEngine Run();
        IMazeRunnerEngine Reset();
    }
}