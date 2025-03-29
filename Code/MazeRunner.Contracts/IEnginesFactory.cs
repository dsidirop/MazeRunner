using System.Collections.Generic;

namespace MazeRunner.Contracts;

public interface IEnginesFactory
{
    IReadOnlyCollection<string> EnginesNames { get; }

    void EnsureInit();
    
    IMazeRunnerEngine Spawn(string enginename, IMaze maze);
}