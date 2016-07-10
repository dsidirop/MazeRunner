using MazeRunner.Shared.Maze;

namespace MazeRunner.SimpleMazeRunner
{
    public class MazeRunnerEngineSimpleDepthFirstPolicy : MazeRunnerEngineDepthFirstPolicyBase
    {
        public MazeRunnerEngineSimpleDepthFirstPolicy(IMaze maze) : base(maze, avoidPathfolding: false)
        {
        }
    }
}