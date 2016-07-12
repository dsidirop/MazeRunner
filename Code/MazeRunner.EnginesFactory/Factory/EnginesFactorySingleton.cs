using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;

namespace MazeRunner.EnginesFactory.Factory
{
    public class EnginesFactorySingleton : IEnginesFactory
    {
        private Dictionary<string, Type> _engines;

        public IReadOnlyCollection<string> EnginesNames
        {
            get
            {
                EnsureInit();
                return _engines.Keys;
            }
        }

        public IMazeRunnerEngine Spawn(string enginename, IMaze maze) //todo integration tests
        {
            EnsureInit();

            var type = (Type) null;
            if (!_engines.TryGetValue(enginename, out type)) throw new ArgumentOutOfRangeException(nameof(enginename));

            return Activator.CreateInstance(type, maze) as IMazeRunnerEngine;
        }

        public void EnsureInit() //0
        {
            if (_engines != null) return;

            lock (_locker)
            {
                if (_engines != null) return;

                _engines = Directory.GetFiles(Utilities.ProductInstallationFolderpath_system, "MazeRunner.Engine.*.dll")
                    .SelectMany(TryLoadAssemblyAndGetExportedTypes)
                    .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(TypeOfIMazeRunnerEngine))
                    .ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);
            }
        }
        //0 play it safe in terms of ensuring threadsafe init

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

        private EnginesFactorySingleton()
        {
        }

        static public EnginesFactorySingleton I => _lazyInstance.Value;
        static private readonly Lazy<EnginesFactorySingleton> _lazyInstance = new Lazy<EnginesFactorySingleton>(() => new EnginesFactorySingleton());
        static private readonly Type TypeOfIMazeRunnerEngine = typeof(IMazeRunnerEngine);

        private readonly object _locker = new object();
    }
}
