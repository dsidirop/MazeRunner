using System.Collections.Generic;
using MazeRunner.Engines.Contracts;
using MazeRunner.Mazes.Contracts;

namespace MazeRunner.EnginesFactory.Contracts;

public interface IGrandMazeRunnersEnginesFactory
{
    IReadOnlyCollection<string> EnginesNames { get; }

    bool EnsureInitializedOnce();
    
    IMazeRunnerEngine Spawn(string enginename, IMaze maze);
}