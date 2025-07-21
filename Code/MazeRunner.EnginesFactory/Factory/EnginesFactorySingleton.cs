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
    public readonly TraceSource Tracer = new(nameof(EnginesFactorySingleton), SourceLevels.Off);

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

#pragma warning disable CA1508
    public void EnsureInit() //0
    {
        if (_engines != null) return;

        lock (_locker)
        {
            if (_engines != null) return;

            _engines = Enumerable
                .Empty<Assembly>()
                .Concat(AppDomain.CurrentDomain.GetAssemblies()) //this works in MAUI (android, ios, ...) and on Windows/Linux
                .Concat(TryGetDllFilesToScanFromFilesystem_().Select(TryLoadAssemblyFile_).Where(a => a != null)) // this works only on Windows/Linux
                .SelectMany(CollectAllExportedTypes_)
                .Where(MatchConcreteClassesOfIMazeRunnerEngine_)
                .ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);

            Tracer.TraceInformation(
                $"""
                 Factory initialization complete. Scanned {TryGetDllFilesToScanFromFilesystem_().Length} dlls:

                 {TryGetDllFilesToScanFromFilesystem_().LineJoinify()}

                 Found {_engines.Count} engines:

                 {EnginesNames.LineJoinify()}
                 """
            );
        }

        return;
        
        static IEnumerable<Type> CollectAllExportedTypes_(Assembly assembly)
        {
            return assembly.GetExportedTypes();
        }

        string[] TryGetDllFilesToScanFromFilesystem_()
        {
            try
            {
                return Directory.GetFiles(U.ProductInstallationFolderpath_system, "MazeRunner.Engine.*.dll");
            }
            catch (Exception ex)
            {
                Tracer.TraceInformation($"Failed to scan for maze runner engine dlls in the installation folder '{U.ProductInstallationFolderpath_system}' (ignoring this and moving on):\n\n{ex}");
                return [];
            }
        }

        static bool MatchConcreteClassesOfIMazeRunnerEngine_(Type x)
        {
            return x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(TypeOfIMazeRunnerEngine);
        }

        Assembly TryLoadAssemblyFile_(string filepath)
        {
            try
            {
                return Assembly.LoadFrom(filepath);
            }
            catch (Exception ex)
            {
                Tracer.TraceInformation($"Failed to load assembly '{filepath}' to scan the engines it provides:\n\n{ex}");
                return null;
            }
        }

        //0 play it safe in terms of ensuring threadsafe init
        //1 scan engines dynamically from all dlls that are named after the pattern mazerunner.engine.xyz.dll   if someone wants to add his own engine he can just
        //  drop his dll into the directory with the rest of the dlls
    }
#pragma warning restore CA1508


    private EnginesFactorySingleton()
    {
    }

    static public EnginesFactorySingleton I => LazyInstance.Value;
    static private readonly Lazy<EnginesFactorySingleton> LazyInstance = new(() => new EnginesFactorySingleton());
    static private readonly Type TypeOfIMazeRunnerEngine = typeof(IMazeRunnerEngine);

    private readonly Lock _locker = new();
}