using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazeRunner.Shared.Engine
{
    public class MazeSquare
    {
        public Point Coords { get; }
        public MazeSquare Parent { get; }

        public List<Point?> AdjacentSquares => Offsets
            .Select(i => new Point(Coords.X - i.X, Coords.Y - i.Y))
            .Cast<Point?>().ToList();

        public MazeSquare(Point coords, MazeSquare parent = null)
        {
            Coords = coords;
            Parent = parent;
        }

        static public bool operator ==(MazeSquare left, MazeSquare right)
        {
            return !ReferenceEquals(left, null) && left.Equals(right);
        }

        static public bool operator !=(MazeSquare left, MazeSquare right)
        {
            return !(left == right); //dont turn this into left != right
        }

        public override int GetHashCode() => Coords.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as MazeSquare);
        protected bool Equals(MazeSquare other) => ReferenceEquals(this, other) || (!ReferenceEquals(other, null) && Coords.Equals(other.Coords));

        static private readonly List<Point> Offsets = new List<Point> {new Point(x: 0, y: -1), new Point(x: 1, y: 0), new Point(x: 0, y: 1), new Point(x: -1, y: 0)};
    }
}