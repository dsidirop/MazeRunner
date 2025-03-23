using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MazeRunner.Contracts;
using MazeRunner.Utils;

namespace MazeRunner.Mazes;

public sealed class Maze : IMaze
{
    public const int MinimumArea = 2;
    public const int MaximumArea = int.MaxValue;

    private readonly Rectangle _rectangle;
    private readonly HashSet<Point> _roadblocks;

    public Size Size => _rectangle.Size;
    public Point Exitpoint { get; }
    public Point Entrypoint { get; }
    public int RoadblocksCount => _roadblocks.Count;

    public bool Contains(Point p) => _rectangle.Contains(p);

    public Maze(Size size, Point entrypoint, Point exitpoint, HashSet<Point> roadblocks)
    {
        ArgumentNullException.ThrowIfNull(roadblocks); //0 order

        var mazeArea = ((double) size.Width)*size.Height;
        if (size.Height <= 0 || size.Width <= 0 || mazeArea < MinimumArea || mazeArea > MaximumArea) throw new ArgumentException(nameof(size)); //order

        var rectangle = new Rectangle(Point.Empty, size);
        if (!rectangle.Contains(exitpoint) || roadblocks.Contains(exitpoint)) throw new ArgumentException("Invalid exitpoint", nameof(exitpoint)); //order
        if (!rectangle.Contains(entrypoint) || roadblocks.Contains(entrypoint)) throw new ArgumentException("Invalid entrypoint", nameof(entrypoint)); //order
        if (entrypoint == exitpoint) throw new ArgumentException("Entrypoint and exitpoint are the same");
        if (roadblocks.Count > size.Height * size.Width - 2 || roadblocks.Any(p => !rectangle.Contains(p))) throw new ArgumentException("Invalid roadblocks", nameof(roadblocks)); //order

        Exitpoint = exitpoint;
        Entrypoint = entrypoint;

        _rectangle = rectangle; //1
        _roadblocks = roadblocks;
    }
    //0 the roadblocks cannot be more than the total number of squares in the map reduced by two squares for the entry point and the exit point
    //1 the rectangle struct has built-in support for the contains method which is neat since it saves us from the trouble of rolling out our own custom methods

    public MazeHitTestEnum HitTest(Point p)
    {
        if (!_rectangle.Contains(p)) return MazeHitTestEnum.Roadblock; //0

        if (p == Exitpoint) return MazeHitTestEnum.Exitpoint;
        if (p == Entrypoint) return MazeHitTestEnum.Entrypoint;

        return _roadblocks.Contains(p) ? MazeHitTestEnum.Roadblock : MazeHitTestEnum.Free;
    }
    //0 it is convenient to regard all points outside the confines of the maze as points comprised solely of roadblocks

    public override string ToString() => $"EntryPoint = {Entrypoint}, ExitPoint = {Exitpoint}, RxC = {Size.Height} x {Size.Width}, WallDensity% = {this.GetMazeSpecs().RoadblockDensity}%";
}