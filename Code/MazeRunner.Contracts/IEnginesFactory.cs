using System.Collections.Generic;

namespace MazeRunner.Contracts;

public interface IEnginesFactory
{
    IReadOnlyCollection<string> EnginesNames { get; }

    bool EnsureInitializedOnce();
    
    IMazeRunnerEngine Spawn(string enginename, IMaze maze);
}