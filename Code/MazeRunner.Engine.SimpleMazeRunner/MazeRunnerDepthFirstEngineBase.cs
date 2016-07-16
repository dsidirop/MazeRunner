using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using MazeRunner.Shared;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
using MazeRunner.Shared.Interfaces.Events;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public abstract class MazeRunnerDepthFirstEngineBase : IMazeRunnerEngine
    {
        private readonly TraceSource Tracer = new TraceSource(nameof(MazeRunnerDepthFirstEngineBase), SourceLevels.Off); //preferred not to use inheritance for setting this

        private event EventHandler _starting;
        public event EventHandler Starting
        {
            add
            {
                _starting -= value;
                _starting += value;
            }
            remove { _starting -= value; }
        }

        private event EventHandler<ConcludedEventArgs> _concluded;
        public event EventHandler<ConcludedEventArgs> Concluded
        {
            add
            {
                _concluded -= value;
                _concluded += value;
            }
            remove { _concluded -= value; }
        }

        private event EventHandler<StateChangedEventArgs> _stateChanged;
        public event EventHandler<StateChangedEventArgs> StateChanged
        {
            add
            {
                _stateChanged -= value;
                _stateChanged += value;
            }
            remove { _stateChanged -= value; }
        }

        private readonly IMaze _maze;
        private readonly bool _avoidPathfolding;
        private readonly HashSet<Point> _invalidatedSquares;
        private readonly ReorderableDictionary<Point, Point> _currentTrajectorySquares;

        public Point? TrajectoryTip
        {
            get { return _currentTrajectorySquares.Any() ? _currentTrajectorySquares[_currentTrajectorySquares.Count - 1] : (Point?) null; }
            private set { _currentTrajectorySquares.Add(value.Value, value.Value); }
        }

        public IMaze Maze => _maze;
        public int TrajectoryLength => _currentTrajectorySquares.Count;
        public IEnumerable<Point> Trajectory => _currentTrajectorySquares.Keys.Cast<Point>(); //no easy way to use ireadonlycollection here
        public IReadOnlyCollection<Point> InvalidatedSquares => _invalidatedSquares;

        protected MazeRunnerDepthFirstEngineBase(IMaze maze, bool avoidPathfolding, TraceSource tracer = null)
        {
            if (maze == null) throw new ArgumentNullException(nameof(maze));

            Tracer = tracer ?? Tracer;

            _maze = maze;
            _avoidPathfolding = avoidPathfolding;
            _invalidatedSquares = new HashSet<Point>();
            _currentTrajectorySquares = new ReorderableDictionary<Point, Point>(); //0
        }
        //0 Currenttrajectorysquares is based on a reorderabledictionary so that the insertion order will be available at all times  A simple dictionary wouldnt
        //  cut it because according to ms documentation plain old dictionaries give no guarantees in terms of reporting their items based on their insertion order

        public IMazeRunnerEngine Reset()
        {
            _invalidatedSquares.Clear();
            _currentTrajectorySquares.Clear();

            return this;
        }

        public IMazeRunnerEngine Run(CancellationToken? cancellationToken = null)
        {
            var ct = cancellationToken ?? CancellationToken.None;

            var si = 1;
            var conclusionStatusType = ConclusionStatusTypeEnum.Completed;
            try
            {
                OnStarting();

                ct.ThrowIfCancellationRequested();

                var tip = TrajectoryTip = _maze.Entrypoint;
                OnStateChanged(new StateChangedEventArgs {StepIndex = si++, OldTip = null, NewTip = tip, IsProgressNotBacktracking = true});
                while (tip != null && _maze.HitTest(tip.Value) != MazeHitTestEnum.Exitpoint)
                {
                    ct.ThrowIfCancellationRequested();

                    var randomValidAdjacentSquare = tip.Value.GetAdjacentPoints().Shuffle().FirstOrDefault(candidateSquare => //enforce depthfirst-prone logic on random adjacent square
                    {
                        return _maze.HitTest(candidateSquare.Value) != MazeHitTestEnum.Roadblock //roadblock or out of maze
                               && !_currentTrajectorySquares.Contains(candidateSquare) //already in trajectory
                               && !_invalidatedSquares.Contains(candidateSquare.Value) //ReSharper disable once AssignNullToNotNullAttribute  already invalidated
                               && (!_avoidPathfolding || candidateSquare.Value.GetAdjacentPoints().Except(new[] {tip}).All(z => !_currentTrajectorySquares.Contains(z))); //to avoid pathfolding we check if the adjacent square is next to a square of the current trajectory other than the current trajectorytip
                    });

                    var newSquareFound = randomValidAdjacentSquare != null;
                    if (newSquareFound) //found an unvisited adjacent square that matches the criteria of our policy
                    {
                        TrajectoryTip = randomValidAdjacentSquare;
                    }
                    else
                    {
                        _invalidatedSquares.Add(tip.Value); //current trajectorytip has no unvisited adjacent squares that match our
                        _currentTrajectorySquares.Remove(tip.Value); //policy so we backtrack by one square in the current trajectory
                    }

                    OnStateChanged(new StateChangedEventArgs
                    {
                        OldTip = tip, //order
                        NewTip = (tip = TrajectoryTip), //order   tip becomes null when we backtrack all the way back before square one and cant backtrack any further
                        StepIndex = si++,
                        IsProgressNotBacktracking = newSquareFound
                    });
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
                OnConcluded(new ConcludedEventArgs {Status = conclusionStatusType, ExitpointReached = conclusionStatusType == ConclusionStatusTypeEnum.Completed && TrajectoryTip != null});
            }
            return this;
        }

        protected virtual void OnStarting()
        {
            Tracer.TraceInformation($"Commencing on maze with specs: {_maze}");

            _starting?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnConcluded(ConcludedEventArgs ea)
        {
            Tracer.TraceInformation($"{ea}");

            _concluded?.Invoke(this, ea);
        }

        protected virtual void OnStateChanged(StateChangedEventArgs ea)
        {
            Tracer.TraceInformation($"{ea}");

            _stateChanged?.Invoke(this, ea);
        }

        private void OnException(int stepIndex, Exception ex)
        {
            if (ex is OperationCanceledException) return; //stop button

            try
            {
                Tracer.TraceEvent(TraceEventType.Error, 0,
                    $"Engine '{this.GetEngineName()}' crashed:{U.nl2}" +
                    $"Step# {stepIndex}{U.nl}" +
                    $"TrajectoryLength: {TrajectoryLength}{U.nl}" +
                    $"InvalidatedSquares({InvalidatedSquares.Count}): {string.Join(", ", InvalidatedSquares.Select(p => $"({p.X}, {p.Y})"))}{U.nl}" +
                    $"CurrentTrajectorySquares: {_currentTrajectorySquares.Keys.Cast<Point>().Select(p => $"({p.X}, {p.Y})")}{U.nl2}" +
                    $"{ex}");
            }
            catch (Exception xx)
            {
                Tracer.TraceEvent(TraceEventType.Error, 0, $"MRDFEB01 [BUG]: {xx}");
            }
        }
    }
}
