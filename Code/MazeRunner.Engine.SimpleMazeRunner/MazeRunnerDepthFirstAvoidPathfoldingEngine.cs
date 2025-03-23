using System.Diagnostics;
using MazeRunner.Contracts;

namespace MazeRunner.Engine.SimpleMazeRunner;

public class MazeRunnerDepthFirstAvoidPathfoldingEngine : MazeRunnerDepthFirstEngineBase
{
    static public readonly TraceSource Tracer = new TraceSource(nameof(MazeRunnerDepthFirstAvoidPathfoldingEngine), SourceLevels.Off);

    public MazeRunnerDepthFirstAvoidPathfoldingEngine(IMaze maze) : base(maze, avoidPathfolding: true, tracer: Tracer)
    {
    }

    public override string GetEngineName()
    {
        return nameof(MazeRunnerDepthFirstAvoidPathfoldingEngine);
    }
}