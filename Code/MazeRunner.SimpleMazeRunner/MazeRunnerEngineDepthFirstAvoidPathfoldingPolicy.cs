using MazeRunner.Shared.Maze;

namespace MazeRunner.SimpleMazeRunner
{
    public class MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy : MazeRunnerEngineDepthFirstPolicyBase
    {
        public MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(IMaze maze) : base(maze, avoidPathfolding: true)
        {
        }
    }
}
