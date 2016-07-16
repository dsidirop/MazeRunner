using System.Drawing;

namespace MazeRunner.Shared.Interfaces
{
    public interface IMaze
    {
        Size Size { get; }
        Point Exitpoint { get; }
        Point Entrypoint { get; }
        int RoadblocksCount { get; }

        bool Contains(Point p);
        MazeHitTestEnum HitTest(Point p);

        string ToString(); //specs
    }
}
