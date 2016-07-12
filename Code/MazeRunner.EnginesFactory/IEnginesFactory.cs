using System.Collections.Generic;
using MazeRunner.Shared.Engine;
using MazeRunner.Shared.Maze;

namespace MazeRunner.EnginesFactory
{
    public interface IEnginesFactory
    {
        IEnumerable<string> EnginesNames { get; }

        void EnsureInit();
        IMazeRunnerEngine Spawn(string enginename, IMaze maze);
    }
}