using System;
using System.Collections.Generic;
using System.Linq;
using MazeRunner.Shared.Engine;
using MazeRunner.Shared.Maze;

namespace MazeRunner.EnginesFactory
{
    public class EnginesFactorySingleton
    {
        private readonly Dictionary<string, Type> _engines;

        public IEnumerable<KeyValuePair<string, Type>> Engines => _engines.AsEnumerable();

        public IMazeRunnerEngine Spawn(string enginename, IMaze maze)
        {
            return null;
        }

        private EnginesFactorySingleton() //threadsafe init
        {
            _engines = new Dictionary<string, Type>(16);
            //todo scan
        }

        static public EnginesFactorySingleton I => _lazyInstance.Value;
        
        static private readonly Lazy<EnginesFactorySingleton> _lazyInstance = new Lazy<EnginesFactorySingleton>(() => new EnginesFactorySingleton());
    }
}
