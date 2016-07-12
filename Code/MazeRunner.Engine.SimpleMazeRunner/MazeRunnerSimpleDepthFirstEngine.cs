using MazeRunner.Shared.Interfaces;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public class MazeRunnerSimpleDepthFirstEngine : MazeRunnerDepthFirstEngineBase
    {
        public MazeRunnerSimpleDepthFirstEngine(IMaze maze) : base(maze, avoidPathfolding: false)
        {
        }
    }
}