using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared
{
    public interface IMaze
    {
        Size Size { get; }
        Point Exitpoint { get; }
        Point Entrypoint { get; }

        MazeHitTestEnum HitTest(Point p);
    }
}
