using MazeRunner.Shared.Maze;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public class MazeRunnerDepthFirstAvoidPathfoldingEngine : MazeRunnerDepthFirstEngineBase
    {
        public MazeRunnerDepthFirstAvoidPathfoldingEngine(IMaze maze) : base(maze, avoidPathfolding: true)
        {
        }
    }
}
