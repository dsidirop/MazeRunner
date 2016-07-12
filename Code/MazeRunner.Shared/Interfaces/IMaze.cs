using System.Drawing;

namespace MazeRunner.Shared.Interfaces
{
    public interface IMaze
    {
        Size Size { get; }
        Point Exitpoint { get; }
        Point Entrypoint { get; }

        MazeHitTestEnum HitTest(Point p);
    }
}
