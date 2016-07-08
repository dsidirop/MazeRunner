using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MazeRunner.Shared;

namespace MazeRunner.Mazes
{
    public sealed class Maze : IMaze
    {
        public Size Size { get; }
        public Point Endpoint { get; }
        public Point Startpoint { get; }
        public HashSet<Point> Roadblocks { get; }

        public Maze(Size size, HashSet<Point> roadblocks, Point startpoint, Point endpoint)
        {
            if (roadblocks == null) throw new ArgumentException(nameof(roadblocks)); //0 order
            if (size.Height <= 0 || size.Width <= 0 || size.Width * size.Height <= 1) throw new ArgumentException(nameof(size)); //order
            if (HealthCheckCoords(size, endpoint) || roadblocks.Contains(endpoint)) throw new ArgumentException(nameof(endpoint)); //order
            if (HealthCheckCoords(size, startpoint) || roadblocks.Contains(startpoint)) throw new ArgumentException(nameof(startpoint)); //order
            if (roadblocks.Any(p => HealthCheckCoords(size, p)) || roadblocks.Count >= size.Height * size.Width - 2) throw new ArgumentException(nameof(roadblocks)); //order

            Size = size;
            Endpoint = endpoint;
            Startpoint = startpoint;
            Roadblocks = roadblocks;
        }
        //0 the roadblocks cannot be more than the total number of squares in the map

        public MazeHitTestEnum HitTest(Point p)
        {
            return MazeHitTestEnum.Exitpoint;
        }

        static private bool HealthCheckCoords(Size s, Point p) => p.X < 0 || p.X >= s.Width || p.Y < 0 || p.Y >= s.Height;
    }
}
