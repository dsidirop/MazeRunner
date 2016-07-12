using System.Collections.Generic;

namespace MazeRunner.Shared.Interfaces
{
    public interface IEnginesFactory
    {
        IReadOnlyCollection<string> EnginesNames { get; }

        void EnsureInit();
        IMazeRunnerEngine Spawn(string enginename, IMaze maze);
    }
}