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

        private MazeSquare _trajectoryTip;

        private readonly IMaze _maze;
        private readonly HashSet<MazeSquare> _deadendSquares;
        private readonly ReorderableDictionary<MazeSquare, MazeSquare> _currentTrajectorySquares;

        public Point TrajectoryTip => _trajectoryTip.Coords;
        public IEnumerable<Point> TestedSquares => _deadendSquares.Select(x => x.Coords);
        public IEnumerable<Point> CurrentTrajectorySquares => _currentTrajectorySquares.Keys.Cast<MazeSquare>().Select(x => x.Coords);

        public MazeRunnerDepthFirstPolicy(IMaze maze)
        {
            if (maze == null) throw new ArgumentNullException(nameof(maze));

            _maze = maze;
            _trajectoryTip = new MazeSquare(maze.Entrypoint, parent: null);
            _deadendSquares = new HashSet<MazeSquare> {_trajectoryTip};
            _currentTrajectorySquares = new ReorderableDictionary<MazeSquare, MazeSquare> {{_trajectoryTip, _trajectoryTip}}; //0
        }
        //0 currenttrajectorysquares is based on a reorderabledictionary so that the insertion order will be respected at all times

        public void Run()
        {
            var success = false;
            while (true)
            {
                var adjacentSquares = _trajectoryTip.AdjacentSquares; //nullable points
                var possibleExitpointFound = adjacentSquares.FirstOrDefault(x => _maze.HitTest(x.Value) == MazeHitTestEnum.Exitpoint); //lookahead one
                if (possibleExitpointFound != null)
                {
                    success = true;
                    _trajectoryTip = new MazeSquare(possibleExitpointFound.Value, parent: _trajectoryTip); //order
                    _currentTrajectorySquares.Add(_trajectoryTip, _trajectoryTip); //order
                    break;
                }

                var randomValidAdjacentSquare = adjacentSquares.Shuffle().FirstOrDefault(x => //enforce depthfirst-prone logic on random adjacent square
                {
                    var candidateSquare = new MazeSquare(x.Value);
                    return _maze.HitTest(candidateSquare.Coords) == MazeHitTestEnum.Roadblock //roadblock or out of maze
                           || _deadendSquares.Contains(candidateSquare) //already visited
                           || candidateSquare.AdjacentSquares.Select(y => new MazeSquare(y.Value)).Any(z => z != _trajectoryTip && _currentTrajectorySquares.Contains(z)); //is next to previous square of the current trajectory other than the current trajectorytip
                });

                if (randomValidAdjacentSquare != null) //found an unvisited adjacent square that matches the criteria of our policy
                {
                    _trajectoryTip = new MazeSquare(randomValidAdjacentSquare.Value, parent: _trajectoryTip); //order
                    _currentTrajectorySquares.Add(_trajectoryTip, _trajectoryTip); //order
                    continue;
                }

                //current trajectorytip has no unvisited adjacent squares that match our policy so we backtrack by one square in the current trajectory
                _deadendSquares.Add(_trajectoryTip); //order
                _currentTrajectorySquares.Remove(_trajectoryTip); //order
                _trajectoryTip = _trajectoryTip.Parent; //order
                if (_trajectoryTip == null) break; //backtracked all the way back to square one and cant backtrack further

                OnEngineStateChanged(); //progress notification
            }

            OnConcluded(new ConcludedEventArgs {Success = success, Trajectory = CurrentTrajectorySquares.ToArray()});
        }

        protected virtual void OnConcluded(ConcludedEventArgs ea) => _concluded?.Invoke(this, ea);
        protected virtual void OnEngineStateChanged() => _engineStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
