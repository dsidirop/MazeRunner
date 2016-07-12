using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MazeRunner.Shared.Engine;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Maze;

namespace MazeRunner.EnginesFactory
{
    public class EnginesFactorySingleton
    {
        private readonly Dictionary<string, Type> _engines;

        public IEnumerable<KeyValuePair<string, Type>> Engines => _engines.AsEnumerable();

        public IMazeRunnerEngine Spawn(string enginename, IMaze maze) //todo integration tests
        {
            var type = (Type) null;
            if (!_engines.TryGetValue(enginename, out type)) throw new ArgumentOutOfRangeException(nameof(enginename));

            return Activator.CreateInstance(type, maze) as IMazeRunnerEngine;
        }

        private EnginesFactorySingleton() //threadsafe init
        {
            _engines = Directory.GetFiles(Utilities.ProductInstallationFolderpath_system, "MazeRunner.Engine.*.dll")
                .SelectMany(TryLoadAssemblyAndGetExportedTypes)
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(TypeOfIMazeRunnerEngine))
                .ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);
        }

        static private Type[] TryLoadAssemblyAndGetExportedTypes(string filepath)
        {
            try
            {
                return Assembly.LoadFrom(filepath).GetExportedTypes();
            }
            catch (Exception ex)
            {
                //todo log
                return new Type[] {};
            }
        }

        static public EnginesFactorySingleton I => _lazyInstance.Value;
        static private readonly Lazy<EnginesFactorySingleton> _lazyInstance = new Lazy<EnginesFactorySingleton>(() => new EnginesFactorySingleton());

        static private readonly Type TypeOfIMazeRunnerEngine = typeof(IMazeRunnerEngine);
    }
}
