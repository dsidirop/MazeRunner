using System.Drawing;
using MazeRunner.Shared.Helpers;

namespace MazeRunner.Shared.Maze
{
    public interface IMaze
    {
        Size Size { get; }
        Point Exitpoint { get; }
        Point Entrypoint { get; }

        MazeHitTestEnum HitTest(Point p);
    }
}
