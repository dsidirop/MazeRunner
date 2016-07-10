using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MazeRunner.Shared.Engine;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Maze;

namespace MazeRunner.SimpleMazeRunner
{
    public class MazeRunnerDepthFirstPolicy : IMazeRunnerEngine
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

        private event EventHandler _engineStateChanged;
        public event EventHandler EngineStateChanged
        {
            add
            {
                _engineStateChanged -= value;
                _engineStateChanged += value;
            }
            remove { _engineStateChanged -= value; }
        }

        private readonly IMaze _maze;
        private readonly HashSet<Point> _invalidatedSquares;
        private readonly ReorderableDictionary<Point, Point> _currentTrajectorySquares;

        public Point? TrajectoryTip
        {
            get { return _currentTrajectorySquares.Any() ? _currentTrajectorySquares[_currentTrajectorySquares.Count - 1] : (Point?) null; }
            private set { _currentTrajectorySquares.Add(value.Value, value.Value); }
        }

        public IEnumerable<Point> InvalidatedSquares => _invalidatedSquares;
        public IEnumerable<Point> CurrentTrajectorySquares => _currentTrajectorySquares.Keys.Cast<Point>();

        public MazeRunnerDepthFirstPolicy(IMaze maze)
        {
            if (maze == null) throw new ArgumentNullException(nameof(maze));

            _maze = maze;
            _invalidatedSquares = new HashSet<Point>();
            _currentTrajectorySquares = new ReorderableDictionary<Point, Point> {{maze.Entrypoint, maze.Entrypoint}}; //0
        }
        //0 Currenttrajectorysquares is based on a reorderabledictionary so that the insertion order will be available at all times
        //  A simple dictionary wouldnt cut it because according to ms documentation the dictionary gives no guarantees in this regard

        public void Run()
        {
            for (var tip = TrajectoryTip; tip != null; tip = TrajectoryTip, OnEngineStateChanged()) //tip becomes null when we backtrack all the way back to square one and cant backtrack further
            {
                var adjacentSquares = tip.Value.GetAdjacentPoints(); //nullable points
                var possibleExitpointFound = adjacentSquares.FirstOrDefault(x => _maze.HitTest(x.Value) == MazeHitTestEnum.Exitpoint); //lookahead one
                if (possibleExitpointFound != null)
                {
                    TrajectoryTip = possibleExitpointFound;
                    break;
                }

                var randomValidAdjacentSquare = adjacentSquares.Shuffle().FirstOrDefault(candidateSquare => //enforce depthfirst-prone logic on random adjacent square
                {
                    return _maze.HitTest(candidateSquare.Value) != MazeHitTestEnum.Roadblock //roadblock or out of maze
                           && !_invalidatedSquares.Contains(candidateSquare.Value) //ReSharper disable once AssignNullToNotNullAttribute  already visited
                           && candidateSquare.Value.GetAdjacentPoints().Except(new [] {tip}).All(z => !_currentTrajectorySquares.Contains(z)); //to avoid pathfolding check if the adjacent square is next to a square of the current trajectory other than the current trajectorytip
                });

                if (randomValidAdjacentSquare != null) //found an unvisited adjacent square that matches the criteria of our policy
                {
                    TrajectoryTip = randomValidAdjacentSquare;
                    continue;
                }

                _invalidatedSquares.Add(tip.Value); //current trajectorytip has no unvisited adjacent squares that match our
                _currentTrajectorySquares.Remove(tip.Value); //policy so we backtrack by one square in the current trajectory
            }

            OnConcluded(new ConcludedEventArgs {Success = TrajectoryTip != null, Trajectory = CurrentTrajectorySquares.ToArray()});
        }

        protected virtual void OnConcluded(ConcludedEventArgs ea) => _concluded?.Invoke(this, ea);
        protected virtual void OnEngineStateChanged() => _engineStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
