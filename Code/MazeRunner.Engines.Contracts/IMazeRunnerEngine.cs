using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MazeRunner.Engines.Contracts.Events;
using MazeRunner.Mazes.Contracts;

namespace MazeRunner.Engines.Contracts;

public interface IMazeRunnerEngine
{
    event EventHandler<LapStartingEventArgs> LapStarting;
    event EventHandler<LapConcludedEventArgs> LapConcluded;
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