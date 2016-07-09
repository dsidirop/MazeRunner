using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeRunner.Shared.Engine
{
    public class ConcludedEventArgs : EventArgs
    {
        public bool Success;
        public IList<Point> Trajectory; //success only
    }
}