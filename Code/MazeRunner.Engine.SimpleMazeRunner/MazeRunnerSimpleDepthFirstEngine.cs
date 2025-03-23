using System.Diagnostics;
using MazeRunner.Shared.Interfaces;

namespace MazeRunner.Engine.SimpleMazeRunner;

public class MazeRunnerSimpleDepthFirstEngine : MazeRunnerDepthFirstEngineBase
{
    static public readonly TraceSource Tracer = new TraceSource(nameof(MazeRunnerSimpleDepthFirstEngine), SourceLevels.Off);

    public MazeRunnerSimpleDepthFirstEngine(IMaze maze) : base(maze, avoidPathfolding: false, tracer: Tracer)
    {
    }
}