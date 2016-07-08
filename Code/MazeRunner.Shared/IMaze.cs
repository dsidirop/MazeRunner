using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared
{
    public interface IMaze
    {
        Size Size { get; }
        Point Endpoint { get; }
        Point Startpoint { get; }
        HashSet<Point> Roadblocks { get; }

        MazeHitTestEnum HitTest(Point p);
    }
}
