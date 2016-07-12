using MazeRunner.Shared.Maze;

namespace MazeRunner.Mazes
{
    public interface IMazesFactory
    {
        IMaze FromFile(string path, bool suppressExceptions = true);
        IMaze Random(int width, int height, double roadblocksDensity = 0.5);
    }
}