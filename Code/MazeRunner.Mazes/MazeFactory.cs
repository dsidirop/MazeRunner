using System;
using MazeRunner.Shared;

namespace MazeRunner.Mazes
{
    public sealed class MazeFactory //singleton
    {
        public IMaze FromFile(string path)
        {
            return null;
        }

        private MazeFactory() //threadsafe init
        {
        }

        static public MazeFactory I => _lazyInstance.Value;
        static private readonly Lazy<MazeFactory> _lazyInstance = new Lazy<MazeFactory>(() => new MazeFactory());
    }
}
