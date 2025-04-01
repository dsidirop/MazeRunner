using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace MazeRunner.Contracts;

public interface IMaze
{
    Size Size { get; }
    Point Exitpoint { get; }
    Point Entrypoint { get; }
    int RoadblocksCount { get; }

    bool Contains(Point p);
    MazeSpecs GetMazeSpecs();
    MazeHitTestEnum HitTest(Point p);

    string ToString(); //specs

    // strictly speaking these methods should be in a separate interface/class
    string ToAsciiMap(Func<Point, char?> freepointEvaluator = null, string linesSeparator = null, CancellationToken? cancellationToken = null);
    IEnumerable<string> ToStreamedAsciiMap(Func<Point, char?> freepointEvaluator = null, string linesSeparator = null, CancellationToken? cancellationToken = null);
}