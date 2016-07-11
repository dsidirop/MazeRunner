using MazeRunner.Shared.Maze;

namespace MazeRunner.Engine.SimpleMazeRunner
{
    public class MazeRunnerEngineSimpleDepthFirstPolicy : MazeRunnerEngineDepthFirstPolicyBase
    {
        public MazeRunnerEngineSimpleDepthFirstPolicy(IMaze maze) : base(maze, avoidPathfolding: false)
        {
        }
    }
}