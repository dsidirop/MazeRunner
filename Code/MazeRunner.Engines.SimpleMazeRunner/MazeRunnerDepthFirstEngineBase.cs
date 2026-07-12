using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using MazeRunner.Engines.Contracts;
using MazeRunner.Engines.Contracts.Events;
using MazeRunner.Mazes.Contracts;
using MazeRunner.Utils;

namespace MazeRunner.Engines.SimpleMazeRunner;

public abstract class MazeRunnerDepthFirstEngineBase : IMazeRunnerEngine
{
    private readonly TraceSource Tracer = new(nameof(MazeRunnerDepthFirstEngineBase), SourceLevels.Off); //preferred not to use inheritance for setting this

    private event EventHandler<LapStartingEventArgs> _lapStarting;
    public event EventHandler<LapStartingEventArgs> LapStarting
    {
        add
        {
            _lapStarting -= value;
            _lapStarting += value;
        }
        remove => _lapStarting -= value;
    }

    private event EventHandler<LapConcludedEventArgs> _lapConcluded;
    public event EventHandler<LapConcludedEventArgs> LapConcluded
    {
        add
        {
            _lapConcluded -= value;
            _lapConcluded += value;
        }
        remove => _lapConcluded -= value;
    }

    private event EventHandler<StateChangedEventArgs> _stateChanged;
    public event EventHandler<StateChangedEventArgs> StateChanged
    {
        add
        {
            _stateChanged -= value;
            _stateChanged += value;
        }
        remove => _stateChanged -= value;
    }

    private readonly IMaze _maze;
    private readonly bool _avoidPathfolding;
    private readonly HashSet<Point> _invalidatedSquares;
    private readonly ReorderableDictionary<Point, Point> _currentTrajectorySquares;

    public Point? TrajectoryTip
    {
        get => _currentTrajectorySquares.Any() ? _currentTrajectorySquares[^1] : null;
        private set => _currentTrajectorySquares.Add(value!.Value, value!.Value);
    }

    public IMaze Maze => _maze;
    public int TrajectoryLength => _currentTrajectorySquares.Count;
    public IEnumerable<Point> Trajectory => _currentTrajectorySquares.Keys.Cast<Point>(); //no easy way to use ireadonlycollection here
    public IReadOnlyCollection<Point> InvalidatedSquares => _invalidatedSquares;

    protected MazeRunnerDepthFirstEngineBase(IMaze maze, bool avoidPathfolding, TraceSource tracer = null)
    {
        Tracer = tracer ?? Tracer;

        _maze = maze ?? throw new ArgumentNullException(nameof(maze));
        _avoidPathfolding = avoidPathfolding;
        _invalidatedSquares = new HashSet<Point>(capacity: Math.Min(256, (maze.Size.Width * maze.Size.Height - maze.RoadblocksCount) / 5));
        _currentTrajectorySquares = new ReorderableDictionary<Point, Point>(capacity: 64); //0
    }
    //0 current-trajectory-squares is based on a reorderable-dictionary so that the insertion order will be available at all times  A simple dictionary wouldnt
    //  cut it because according to ms documentation plain old dictionaries give no guarantees in terms of reporting their items based on their insertion order

    public IMazeRunnerEngine Reset()
    {
        _invalidatedSquares.Clear();
        _currentTrajectorySquares.Clear();

        return this;
    }

    public abstract string GetEngineName();

    public IMazeRunnerEngine Run(CancellationToken? cancellationToken = null)
    {
        var ct = cancellationToken ?? CancellationToken.None;

        var si = 1;
        var conclusionStatusType = ConclusionStatusTypeEnum.Completed;
        try
        {
            OnLapStarting();

            ct.ThrowIfCancellationRequested();

            var tip = TrajectoryTip = _maze.Entrypoint;
            OnStateChanged(new StateChangedEventArgs(
                oldTip: null,
                newTip: tip,
                stepIndex: si++,
                isProgressNotBacktracking: true
            ));
            while (tip != null && _maze.HitTest(tip.Value) != MazeHitTestEnum.Exitpoint)
            {
                ct.ThrowIfCancellationRequested();

                var randomValidAdjacentSquare = tip.Value.GetAdjacentPoints().ToArray().Shuffle().Cast<Point?>().FirstOrDefault(candidateSquare => //enforce depth-first-prone logic on random adjacent square
                {
                    return _maze.HitTest(candidateSquare!.Value) != MazeHitTestEnum.Roadblock //roadblock or out of maze
                           && !_currentTrajectorySquares.Contains(candidateSquare) //already in trajectory
                           && !_invalidatedSquares.Contains(candidateSquare.Value) //ReSharper disable once AssignNullToNotNullAttribute  already invalidated
                           && (!_avoidPathfolding || candidateSquare.Value.GetAdjacentPoints().Except([tip.Value]).All(z => !_currentTrajectorySquares.Contains(z))); //to avoid pathfolding we check if the adjacent square is next to a square of the current trajectory other than the current trajectorytip
                });

                var newSquareFound = randomValidAdjacentSquare != null;
                if (newSquareFound) //found an unvisited adjacent square that matches the criteria of our policy
                {
                    TrajectoryTip = randomValidAdjacentSquare;
                }
                else
                {
                    _invalidatedSquares.Add(tip.Value); //current trajectory-tip has no unvisited adjacent squares that match our
                    _currentTrajectorySquares.Remove(tip.Value); //policy so we backtrack by one square in the current trajectory
                }

                OnStateChanged(new StateChangedEventArgs(
                    oldTip: tip, //order
                    newTip: tip = TrajectoryTip, //order   tip becomes null when we backtrack all the way back before square one and cant backtrack any further
                    stepIndex: si++,
                    isProgressNotBacktracking: newSquareFound
                ));
            }
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                conclusionStatusType = ConclusionStatusTypeEnum.Stopped;
            }
            else
            {
                OnException(si, ex);
                conclusionStatusType = ConclusionStatusTypeEnum.Crashed;
                throw;
            }
        }
        finally
        {
            OnLapConcluded(new LapConcludedEventArgs(
                status: conclusionStatusType,
                exitpointReached: conclusionStatusType == ConclusionStatusTypeEnum.Completed && TrajectoryTip != null
            ));
        }
        return this;
    }

    protected virtual void OnLapStarting()
    {
        Tracer.TraceInformation($"Commencing on maze with specs: {_maze}");

        _lapStarting?.Invoke(this, new());
    }

    protected virtual void OnLapConcluded(in LapConcludedEventArgs ea)
    {
        Tracer.TraceInformation($"{ea}");

        _lapConcluded?.Invoke(this, ea);
    }

    protected virtual void OnStateChanged(in StateChangedEventArgs ea)
    {
        Tracer.TraceInformation($"{ea}");

        _stateChanged?.Invoke(this, ea);
    }

    private void OnException(int stepIndex, Exception ex)
    {
        if (ex is OperationCanceledException) return; //stop button

        try
        {
            Tracer.TraceEvent(
                id: 0,
                eventType: TraceEventType.Error,
                message: $"""
                          Engine '{GetEngineName()}' crashed:

                          Step# {stepIndex}
                          TrajectoryLength: {TrajectoryLength}
                          InvalidatedSquares({InvalidatedSquares.Count}): {InvalidatedSquares.Select(p => $"({p.X}, {p.Y})").CommaJoinify()}
                          CurrentTrajectorySquares: {_currentTrajectorySquares.Keys.Cast<Point>().Select(p => $"({p.X}, {p.Y})")}

                          {ex}
                          """
            );
        }
        catch (Exception xx)
        {
            Tracer.TraceEvent(TraceEventType.Error, 0, $"MRDFEB01 [BUG]: {xx}");
        }
    }
}