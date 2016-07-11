using MazeRunner.Shared.Maze;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public class MazeRunnerSimpleDepthFirstEngine : MazeRunnerDepthFirstEngineBase
    {
        public MazeRunnerSimpleDepthFirstEngine(IMaze maze) : base(maze, avoidPathfolding: false)
        {
        }
    }
}