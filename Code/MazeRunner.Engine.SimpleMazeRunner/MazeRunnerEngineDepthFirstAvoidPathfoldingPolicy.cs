using MazeRunner.Shared.Maze;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public class MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy : MazeRunnerEngineDepthFirstPolicyBase
    {
        public MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(IMaze maze) : base(maze, avoidPathfolding: true)
        {
        }
    }
}
