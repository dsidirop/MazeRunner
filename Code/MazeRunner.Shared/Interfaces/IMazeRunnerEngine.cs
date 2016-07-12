using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared.Interfaces
{
    public interface IMazeRunnerEngine
    {
        event EventHandler Starting;
        event EventHandler StateChanged; //progressed
        event EventHandler<ConcludedEventArgs> Concluded;

        IMaze Maze { get; }

        int TrajectoryLength { get; }
        Point? TrajectoryTip { get; }
        IEnumerable<Point> Trajectory { get; }
        IReadOnlyCollection<Point> InvalidatedSquares { get; }

        IMazeRunnerEngine Run();
        IMazeRunnerEngine Reset();
    }
}