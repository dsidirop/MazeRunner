using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using MazeRunner.Injectors.Autofac.Extensions;
using MazeRunner.Injectors.Contracts;
using Microsoft.Extensions.FileSystemGlobbing;
using AutofacModule = Autofac.Module;

namespace MazeRunner.Injectors.Autofac;

[UsedImplicitly]
public class AutofacInjectorScannerService : IInjectorScannerService
{
    public readonly TraceSource Tracer = new(nameof(AutofacInjectorScannerService), SourceLevels.Off);

    public ContainerBuilder TryScanAllAssembliesForInjectionsConfigs(ContainerBuilder? preExistingContainerBuilder = null, InjectorAssembliesScannerOptions? options = null)
    {
        options ??= new InjectorAssembliesScannerOptions();

        if (options is {IsFilesystemAssemblyScanningEnabled: false, IsDomainAssembliesScanningEnabled: false}) //order
        {
            Tracer.TraceInformation("[AISC.TSSAAD.010] Assembly scanning has been completely disabled - the grand-factory will be left intentionally dud.");
            return preExistingContainerBuilder ?? new ContainerBuilder();
        }

        try
        {
            var currentDomainPreloadedAssemblies = TryGetCurrentDomainPreloadedAssemblies_(this, options);
            var assembliesFromTheInstallationFolders = TryGetDesiredAssembliesFromFilesystem_(this, options);

            var containerBuilder = preExistingContainerBuilder ?? new ContainerBuilder();

            try
            {
                return Enumerable
                    .Empty<Assembly>()
                    .Concat(second: currentDomainPreloadedAssemblies) //this works in MAUI (android, ios, ...) and on Windows/Linux
                    .Concat(second: assembliesFromTheInstallationFolders) // this works only on Windows/Linux
                    .Distinct() //order   we dont want duplicate assemblies here
                    .Aggregate( //return the preexisting-container-builder enriched with the modules from the assemblies
                        seed: containerBuilder,
                        func: (acc, assembly) =>
                        {
                            containerBuilder.RegisterAssemblyModules(typeof(AutofacModule), assembly);
                            return acc;
                        }
                    );
            }
            finally
            {
                Tracer.TraceInformation(
                    $"""
                     {nameof(AutofacInjectorScannerService)} initialization is complete. Analyzed {currentDomainPreloadedAssemblies.Length + assembliesFromTheInstallationFolders.Length} assemblies in total:

                     - Current Domain Preloaded Assemblies (count={currentDomainPreloadedAssemblies.Length}):

                       {currentDomainPreloadedAssemblies.NullIfEmpty()?.Select(x => x!.ToString()).Joinify("\n  ") ?? "(None)\n"}

                     - Assemblies from the Installation Folders (count={assembliesFromTheInstallationFolders.Length}):

                       {assembliesFromTheInstallationFolders.NullIfEmpty()?.Select(x => x!.ToString()).Joinify("\n  ") ?? "(None)\n"}
                     """
                );
            }
        }
        catch (Exception ex)
        {
            Tracer.TraceInformation($"[AISC.TSSAAD.010] [🐞 BUG 🐞] [RECOVERED] Failed to scan assemblies for autofac modules with the options given. Report this incident!\n\n{ex}");

            return preExistingContainerBuilder ?? new ContainerBuilder();
        }

        static Assembly[] TryGetDesiredAssembliesFromFilesystem_(AutofacInjectorScannerService self, InjectorAssembliesScannerOptions options)
        {
            if (!options.IsFilesystemAssemblyScanningEnabled) //order
                return []; //if the scanning is disabled we return an empty array

            var productInstallationFolderpath = TryGetProductInstallationFolderpath_(self); //order
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

                var whitelistedFiles = ScanDirectoryForAssemblyFilesUsingGlob_(productInstallationFolderpath, options.WhitelistedAssembliesFilesGlobPattern.NullIfDud() ?? "*", fileEnumerationOptions);
                var blacklistedFiles = ScanDirectoryForAssemblyFilesUsingGlob_(productInstallationFolderpath, options.BlacklistedAssembliesFileGlobPattern, fileEnumerationOptions);

                return whitelistedFiles
                    .Except(blacklistedFiles)
                    .Select(s => TryLoadAssemblyFile_(self, s))
                    .Where(a => a != null)
                    .Cast<Assembly>()
                    .ToArray();
            }
            catch (Exception ex)
            {
                self.Tracer.TraceInformation(
                    $"[AISC.TSSAAD.TGDFTSFF.010] [🐟 THIS LOOKS FISHY 🐟] [RECOVERED] Failed to get assembly files from the product " +
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

        static string TryGetProductInstallationFolderpath_(AutofacInjectorScannerService self)
        {
            try
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            }
            catch (Exception ex)
            {
                self.Tracer.TraceInformation(
                    $"[AISC.TSSAAD.TGPIF.010] [🐟 THIS LOOKS FISHY 🐟] [RECOVERED] Failed to determine the " +
                    $"product installation folderpath (in platforms such as iOS this is to be expected though)\n\n{ex}"
                );
                return "";
            }
        }

        static Assembly? TryLoadAssemblyFile_(AutofacInjectorScannerService self, string filepath_)
        {
            try
            {
                return Assembly.LoadFrom(filepath_);
            }
            catch (Exception ex)
            {
                self.Tracer.TraceInformation(
                    $"[AISC.TSSAAD.TLAF.010] [🐟 THIS LOOKS FISHY 🐟] Failed to load assembly '{filepath_}' to scan " +
                    $"the subfactories it provides (in platforms such as iOS this is to be expected though)\n\n{ex}"
                );
                return null;
            }
        }

        static Assembly[] TryGetCurrentDomainPreloadedAssemblies_(AutofacInjectorScannerService self, InjectorAssembliesScannerOptions options)
        {
            if (!options.IsDomainAssembliesScanningEnabled) //order
                return []; //if the scanning is disabled we return an empty array

            var matcher = new Matcher(StringComparison.OrdinalIgnoreCase); //.InvariantCulture is not supported by the matcher

            matcher.AddIncludePatterns( //keep outside the try-catch block   we dont want to suppress exceptions from the matcher itself
                options
                    .WhitelistedDomainAssembliesGlobPatterns
                    .NullIfDud()
                    ?.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim()) ?? ["*"]
            );

            matcher.AddExcludePatterns(
                options
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
                self.Tracer.TraceInformation($"[AISC.TSSAAD.TGCDPA.010] [🐟 THIS LOOKS FISHY 🐟] Failed to get the current domain preloaded assemblies\n\n{ex}");
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
