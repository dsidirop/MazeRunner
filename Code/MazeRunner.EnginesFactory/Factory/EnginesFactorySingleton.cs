using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using MazeRunner.Contracts;
using MazeRunner.Utils;

namespace MazeRunner.EnginesFactory.Factory;

public class EnginesFactorySingleton : IEnginesFactory
{
    public readonly TraceSource Tracer = new TraceSource(nameof(EnginesFactorySingleton), SourceLevels.Off);

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

        if (!_engines.TryGetValue(enginename?.Trim() ?? "", out var type))
            throw new ArgumentOutOfRangeException(nameof(enginename));

        return Activator.CreateInstance(type, maze) as IMazeRunnerEngine;
    }

    public void EnsureInit() //0
    {
        if (_engines != null) return;

        lock (_locker)
        {
            if (_engines != null) return;

            var dllFilesToScan = Directory.GetFiles(U.ProductInstallationFolderpath_system, "MazeRunner.Engine.*.dll");

            _engines = dllFilesToScan //1
                .SelectMany(TryLoadAssemblyAndGetExportedTypes)
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(TypeOfIMazeRunnerEngine))
                .ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);

            Tracer.TraceInformation($"Factory initialization complete. Scanned {dllFilesToScan.Length} dlls:{U.nl2}{string.Join(U.nl, dllFilesToScan)}{U.nl2}Found {_engines.Count} engines:{U.nl2}{string.Join(U.nl, EnginesNames)}");
        }
    }
    //0 play it safe in terms of ensuring threadsafe init
    //1 scan engines dynamically from all dlls that are named after the pattern mazerunner.engine.xyz.dll   if someone wants to add his own engine he can just
    //  drop his dll into the directory with the rest of the dlls

    private Type[] TryLoadAssemblyAndGetExportedTypes(string filepath)
    {
        try
        {
            return Assembly.LoadFrom(filepath).GetExportedTypes();
        }
        catch (Exception ex)
        {
            Tracer.TraceInformation($"Failed to load assembly '{filepath}' to scan the engines it provides:{U.nl2}{ex}");
            return [];
        }
    }

    private EnginesFactorySingleton()
    {
    }

    static public EnginesFactorySingleton I => _lazyInstance.Value;
    static private readonly Lazy<EnginesFactorySingleton> _lazyInstance = new Lazy<EnginesFactorySingleton>(() => new EnginesFactorySingleton());
    static private readonly Type TypeOfIMazeRunnerEngine = typeof(IMazeRunnerEngine);

    private readonly Lock _locker = new Lock();
}