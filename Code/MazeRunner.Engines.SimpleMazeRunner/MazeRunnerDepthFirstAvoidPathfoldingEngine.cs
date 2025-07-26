using System.Diagnostics;
using MazeRunner.Mazes.Contracts;

namespace MazeRunner.Engines.SimpleMazeRunner;

public class MazeRunnerDepthFirstAvoidPathfoldingEngine : MazeRunnerDepthFirstEngineBase
{
    static public readonly TraceSource Tracer = new(nameof(MazeRunnerDepthFirstAvoidPathfoldingEngine), SourceLevels.Off);

    public MazeRunnerDepthFirstAvoidPathfoldingEngine(IMaze maze) : base(maze, avoidPathfolding: true, tracer: Tracer)
    {
    }

    public override string GetEngineName()
    {
        return nameof(MazeRunnerDepthFirstAvoidPathfoldingEngine);
    }
}