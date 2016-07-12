using MazeRunner.Shared.Interfaces;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public class MazeRunnerDepthFirstAvoidPathfoldingEngine : MazeRunnerDepthFirstEngineBase
    {
        public MazeRunnerDepthFirstAvoidPathfoldingEngine(IMaze maze) : base(maze, avoidPathfolding: true)
        {
        }
    }
}
