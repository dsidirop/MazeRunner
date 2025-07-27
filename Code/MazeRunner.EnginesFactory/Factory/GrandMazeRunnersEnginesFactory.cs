#nullable enable

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using MazeRunner.Engines.Contracts;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.Mazes.Contracts;
using MazeRunner.Utils;
using Microsoft.Extensions.FileSystemGlobbing;

namespace MazeRunner.EnginesFactory.Factory;

public class GrandMazeRunnersEnginesFactory : IGrandMazeRunnersEnginesFactory
{
    public readonly TraceSource Tracer = new(nameof(GrandMazeRunnersEnginesFactory), SourceLevels.Off);

    public readonly Lazy<(bool InitializationSuccessful, FrozenDictionary<string, Type> MazeRunnerEnginesRegistry)> LazyCore;
    
    protected readonly GrandMazeRunnerEnginesFactoryOptions Options = new();

    public IReadOnlyCollection<string> EnginesNames => LazyCore.Value.MazeRunnerEnginesRegistry.Keys;

    public GrandMazeRunnersEnginesFactory(GrandMazeRunnerEnginesFactoryOptions? options = null)
    {
        Options = (options ?? Options).Validate(); //order
        
        LazyCore = new(() => TryScanAllAssembliesForSubfactories(this));
    }

    static public GrandMazeRunnersEnginesFactory I => LazyInstance.Value; //todo   remove this once we have proper DI in place
    static private readonly Lazy<GrandMazeRunnersEnginesFactory> LazyInstance = new(() => new GrandMazeRunnersEnginesFactory()); //todo   remove this once we have proper DI in place
    
    public IMazeRunnerEngine Spawn(string enginename, IMaze maze) //todo integration tests
    {
        ArgumentNullException.ThrowIfNull(enginename);

        if (!EnsureInitializedOnce())
            throw new InvalidOperationException("The factory failed to initialize properly");

        if (!LazyCore.Value.MazeRunnerEnginesRegistry.TryGetValue(enginename.Trim(), out var type))
            throw new ArgumentOutOfRangeException(nameof(enginename));

        return (IMazeRunnerEngine) Activator.CreateInstance(type, maze)!;
    }
    
    public bool EnsureInitializedOnce()
    {
        _ = LazyCore.Value; //force initialization of the lazy via TryScanAllAssembliesForSubfactories()   this is threadsafe of course

        return LazyCore.Value.InitializationSuccessful;
    }

    static private (bool InitializationSucceeded, FrozenDictionary<string, Type> SubfactoriesRegistry) TryScanAllAssembliesForSubfactories(GrandMazeRunnersEnginesFactory factory) //0
    {
        if (factory.Options is {IsFilesystemAssemblyScanningEnabled: false, IsDomainAssembliesScanningEnabled: false}) //order
        {
            factory.Tracer.TraceInformation("[GMREF.TSSAAD.010] Assembly scanning has been completely disabled - the grand-factory will be left intentionally dud.");
            return (InitializationSucceeded: true, SubfactoriesRegistry: FrozenDictionary<string, Type>.Empty);
        }

        try
        {
            var currentDomainPreloadedAssemblies = TryGetCurrentDomainPreloadedAssemblies_(factory);
            var assembliesFromTheInstallationFolders = TryGetDesiredAssembliesFromFilesystem_(factory);

            var subfactories = Enumerable
                .Empty<Assembly>()
                .Concat(currentDomainPreloadedAssemblies) //this works in MAUI (android, ios, ...) and on Windows/Linux
                .Concat(assembliesFromTheInstallationFolders) // this works only on Windows/Linux
                .Distinct() //order   we dont want duplicate assemblies here
                .SelectMany(assembly => TryCollectAllExportedTypes_(factory, assembly))
                .Where(type => TryMatchConcreteClassesOfIMazeRunnerEngine_(factory, type))
                .Distinct() //order   we dont want duplicate types    should be impossible but just in case because frozen-dictionary below would crash otherwise
                .ToFrozenDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);

            factory.Tracer.TraceInformation(
                $"""
                 {nameof(GrandMazeRunnersEnginesFactory)} initialization is complete. Analyzed {currentDomainPreloadedAssemblies.Length + assembliesFromTheInstallationFolders.Length} assemblies in total:

                 - Current Domain Preloaded Assemblies (count={currentDomainPreloadedAssemblies.Length}):

                   {currentDomainPreloadedAssemblies.NullIfEmpty()?.Select(x => x!.ToString()).Joinify("\n  ") ?? "(None)\n"}

                 - Assemblies from the Installation Folders (count={assembliesFromTheInstallationFolders.Length}):

                   {assembliesFromTheInstallationFolders.NullIfEmpty()?.Select(x => x!.ToString()).Joinify("\n  ") ?? "(None)\n"}

                 And located {subfactories.Count} subfactories:

                   {subfactories.Keys.NullIfEmpty()?.Joinify("\n  ") ?? "(None)\n"}
                 """
            );

            return (InitializationSucceeded: true, SubfactoriesRegistry: subfactories);
        }
        catch (Exception ex)
        {
            factory.Tracer.TraceInformation(
                $"[GMREF.TSSAAD.010] [🐞 BUG 🐞] [RECOVERED] Failed to scan for subfactories of type " +
                $"'{nameof(IMazeRunnerEngine)}' across all dlls of this app. Report this incident!\n\n{ex}"
            );

            return (
                InitializationSucceeded: false,
                SubfactoriesRegistry: FrozenDictionary<string, Type>.Empty
            );
        }

        static IEnumerable<Type> TryCollectAllExportedTypes_(GrandMazeRunnersEnginesFactory factory, Assembly assembly_)
        {
            try
            {
                return assembly_.GetExportedTypes();
            }
            catch (Exception ex)
            {
                factory.Tracer.TraceInformation($"[GMREF.TSSAAD.CTET.010] [🐞 BUG 🐞] [RECOVERED] Failed to collect exported types from assembly '{assembly_.FullName}'\n\n{ex}");
                return [];
            }
        }

        static Assembly[] TryGetDesiredAssembliesFromFilesystem_(GrandMazeRunnersEnginesFactory factory)
        {
            if (!factory.Options.IsFilesystemAssemblyScanningEnabled) //order
                return []; //if the scanning is disabled we return an empty array

            var productInstallationFolderpath = TryGetProductInstallationFolderpath_(factory); //order
            if (string.IsNullOrWhiteSpace(productInstallationFolderpath)) //an error occurred while trying to determine the product installation folderpath
                return [];

            try
            {
                var fileEnumerationOptions = new EnumerationOptions
                {
                    MatchCasing = MatchCasing.CaseInsensitive,
                    AttributesToSkip = FileAttributes.Hidden | FileAttributes.System | FileAttributes.Temporary,
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = false, //00
                };

                var whitelistedFiles = ScanDirectoryForAssemblyFilesUsingGlob_(productInstallationFolderpath, factory.Options.WhitelistedAssembliesFilesGlobPattern.NullIfDud() ?? "*", fileEnumerationOptions);
                var blacklistedFiles = ScanDirectoryForAssemblyFilesUsingGlob_(productInstallationFolderpath, factory.Options.BlacklistedAssembliesFileGlobPattern, fileEnumerationOptions);

                return whitelistedFiles
                    .Except(blacklistedFiles)
                    .Select(s => TryLoadAssemblyFile_(factory, s))
                    .Where(a => a != null)
                    .Cast<Assembly>()
                    .ToArray();
            }
            catch (Exception ex)
            {
                factory.Tracer.TraceInformation(
                    $"[GMREF.TSSAAD.TGDFTSFF.010] [🐟 THIS LOOKS FISHY 🐟] [RECOVERED] Failed to get assembly files from the product " +
                    $"installation folderpath '{productInstallationFolderpath}' (in platforms such as iOS this is to be expected though)\n\n{ex}"
                );
                return [];
            }

            static IEnumerable<string> ScanDirectoryForAssemblyFilesUsingGlob_(string productInstallationFolderpath_, string globFilePatternWithSemicolons_, EnumerationOptions fileEnumerationOptions_)
            {
                return globFilePatternWithSemicolons_
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(globPattern => globPattern.Trim())
                    .SelectMany(globPattern => Directory.GetFiles(productInstallationFolderpath_, globPattern, fileEnumerationOptions_)); //files
            }

            //00   we dont want to recurse subdirectories here
        }

        static string TryGetProductInstallationFolderpath_(GrandMazeRunnersEnginesFactory factory)
        {
            try
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            }
            catch (Exception ex)
            {
                factory.Tracer.TraceInformation(
                    $"[GMREF.TSSAAD.TGPIF.010] [🐟 THIS LOOKS FISHY 🐟] [RECOVERED] Failed to determine the " +
                    $"product installation folderpath (in platforms such as iOS this is to be expected though)\n\n{ex}"
                );
                return "";
            }
        }

        static bool TryMatchConcreteClassesOfIMazeRunnerEngine_(GrandMazeRunnersEnginesFactory factory, Type x)
        {
            try
            {
                return x is {IsClass: true, IsAbstract: false}
                       && x != typeof(GrandMazeRunnersEnginesFactory) //vital   we dont want the grand-factory itself to be included in the subfactories!
                       && x.GetInterfaces().Contains(typeof(IMazeRunnerEngine))
                       && !string.IsNullOrWhiteSpace(x.AssemblyQualifiedName);
            }
            catch (Exception ex)
            {
                factory.Tracer.TraceInformation(
                    $"[GMREF.TSSAAD.MCCISCF.010] [🐟 THIS LOOKS FISHY 🐟] Failed to analyze type '{x.FullName}' " +
                    $"to see if it matches '{nameof(IMazeRunnerEngine)}'. Report this incident!\n\n{ex}"
                );
                return false;
            }
        }

        static Assembly? TryLoadAssemblyFile_(GrandMazeRunnersEnginesFactory factory, string filepath_)
        {
            try
            {
                return Assembly.LoadFrom(filepath_);
            }
            catch (Exception ex)
            {
                factory.Tracer.TraceInformation(
                    $"[GMREF.TSSAAD.TLAF.010] [🐟 THIS LOOKS FISHY 🐟] Failed to load assembly '{filepath_}' to scan " +
                    $"the subfactories it provides (in platforms such as iOS this is to be expected though)\n\n{ex}"
                );
                return null;
            }
        }

        static Assembly[] TryGetCurrentDomainPreloadedAssemblies_(GrandMazeRunnersEnginesFactory factory)
        {
            if (!factory.Options.IsDomainAssembliesScanningEnabled) //order
                return []; //if the scanning is disabled we return an empty array

            var matcher = new Matcher(StringComparison.OrdinalIgnoreCase); //.InvariantCulture is not supported by the matcher

            matcher.AddIncludePatterns( //keep outside the try-catch block   we dont want to suppress exceptions from the matcher itself
                factory
                    .Options
                    .WhitelistedDomainAssembliesGlobPatterns
                    .NullIfDud()
                    ?.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim()) ?? ["*"]
            );

            matcher.AddExcludePatterns(
                factory
                    .Options
                    .BlacklistedDomainAssembliesGlobPatterns
                    .NullIfDud()
                    ?.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim()) ?? []
            );

            try
            {
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.FullName) && matcher.Match(x.GetName().Name ?? "").HasMatches)
                    .ToArray(); //00
            }
            catch (Exception ex)
            {
                factory.Tracer.TraceInformation($"[GMREF.TSSAAD.TGCDPA.010] [🐟 THIS LOOKS FISHY 🐟] Failed to get the current domain preloaded assemblies\n\n{ex}");
                return [];
            }

            //00  it is generally a good practice to skip dynamic assemblies when scanning for domain-specific types for several reasons:
            //
            //    1. [relevance] dynamic assemblies are typically generated by frameworks for internal purposes (proxies, expression trees) and rarely contain user-defined implementations
            //    2. [stability] dynamic assemblies may have incomplete type information or throw exceptions during reflection operations
            //    3. [performance] dynamic assemblies are often numerous and scanning them can be costly
            //    4. [predictability] our factory discovery mechanism should be deterministic while dynamic assemblies can vary between runs
        }
    }
}
