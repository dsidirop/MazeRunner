using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MazeRunner.Contracts.Events;

namespace MazeRunner.Contracts;

public interface IMazeRunnerEngine
{
    event EventHandler Starting;
    event EventHandler<ConcludedEventArgs> Concluded;
    event EventHandler<StateChangedEventArgs> StateChanged; //progressed

    IMaze Maze { get; }

    int TrajectoryLength { get; }
    Point? TrajectoryTip { get; }
    IEnumerable<Point> Trajectory { get; }
    IReadOnlyCollection<Point> InvalidatedSquares { get; }

    IMazeRunnerEngine Run(CancellationToken? ct = null);
    IMazeRunnerEngine Reset();

    string GetEngineName();
}