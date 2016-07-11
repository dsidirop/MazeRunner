using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MazeRunner.Shared.Engine;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Maze;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public abstract class MazeRunnerDepthFirstEngineBase : IMazeRunnerEngine
    {
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

        private event EventHandler _stateChanged;
        public event EventHandler StateChanged
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

        public int TrajectoryLength => _currentTrajectorySquares.Count;
        public IEnumerable<Point> Trajectory => _currentTrajectorySquares.Keys.Cast<Point>();
        public IEnumerable<Point> InvalidatedSquares => _invalidatedSquares;

        protected MazeRunnerDepthFirstEngineBase(IMaze maze, bool avoidPathfolding)
        {
            if (maze == null) throw new ArgumentNullException(nameof(maze));

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

        public IMazeRunnerEngine Run()
        {
            for (var tip = TrajectoryTip = _maze.Entrypoint; tip != null; tip = TrajectoryTip, OnEngineStateChanged()) //tip becomes null when we backtrack all the way back before square one and cant backtrack any further
            {
                var adjacentSquares = tip.Value.GetAdjacentPoints(); //nullable points
                var possibleExitpointFound = adjacentSquares.FirstOrDefault(x => _maze.HitTest(x.Value) == MazeHitTestEnum.Exitpoint); //lookahead one
                if (possibleExitpointFound != null)
                {
                    TrajectoryTip = possibleExitpointFound;
                    OnEngineStateChanged();
                    break;
                }

                var randomValidAdjacentSquare = adjacentSquares.Shuffle().FirstOrDefault(candidateSquare => //enforce depthfirst-prone logic on random adjacent square
                {
                    return _maze.HitTest(candidateSquare.Value) != MazeHitTestEnum.Roadblock //roadblock or out of maze
                           && !_currentTrajectorySquares.Contains(candidateSquare) //already in trajectory
                           && !_invalidatedSquares.Contains(candidateSquare.Value) //ReSharper disable once AssignNullToNotNullAttribute  already invalidated
                           && (!_avoidPathfolding || candidateSquare.Value.GetAdjacentPoints().Except(new[] {tip}).All(z => !_currentTrajectorySquares.Contains(z))); //to avoid pathfolding we check if the adjacent square is next to a square of the current trajectory other than the current trajectorytip
                });

                if (randomValidAdjacentSquare != null) //found an unvisited adjacent square that matches the criteria of our policy
                {
                    TrajectoryTip = randomValidAdjacentSquare;
                    continue;
                }

                _invalidatedSquares.Add(tip.Value); //current trajectorytip has no unvisited adjacent squares that match our
                _currentTrajectorySquares.Remove(tip.Value); //policy so we backtrack by one square in the current trajectory
            }

            OnConcluded(new ConcludedEventArgs {Success = TrajectoryTip != null, Trajectory = Trajectory.ToArray()});
            return this;
        }

        protected virtual void OnConcluded(ConcludedEventArgs ea) => _concluded?.Invoke(this, ea);
        protected virtual void OnEngineStateChanged() => _stateChanged?.Invoke(this, EventArgs.Empty);
    }
}
