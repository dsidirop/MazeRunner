namespace MazeRunner.EnginesFactory.Factory;

public class EnginesFactoryOptions
{
    /// <summary>Specifies whether the GrandEnginesFactory should scan the current AppDomain for preloaded assemblies.</summary>
    public bool IsDomainAssembliesScanningEnabled { get; set; } = true;

    /// <summary>
    /// Specifies the semicolon-separated whitelist-glob-patterns (case-insensitive by default) to use when scanning for assemblies in the filesystem.
    /// Default is "MazeRunner.*.dll". Setting this to empty is equivalent to setting it to "*" and will match all files.
    /// </summary>
    public string WhitelistedDomainAssembliesGlobPatterns { get; set; } = "MazeRunner.*";
    
    /// <summary>
    /// Specifies the semicolon-separated the blacklist-glob-patterns (case-insensitive by default) to use when scanning for assemblies in the filesystem.
    /// Setting this to empty is equivalent to completely disabling blacklisting altogether
    /// </summary>
    public string BlacklistedDomainAssembliesGlobPatterns { get; set; } = "*test;*tests";


    /// <summary>
    /// Specifies whether the GrandEnginesFactory should scan the filesystem for assemblies that match the <see cref="WhitelistedAssembliesFilesGlobPattern"/>.
    /// Note that this option has no effect in platforms that do not support filesystem access, such as Blazor WebAssembly or iOS.
    /// </summary>
    public bool IsFilesystemAssemblyScanningEnabled { get; set; } = true;

    /// <summary>
    /// Specifies the semicolon-separated whitelist-glob-patterns (case-insensitive by default) to use when scanning for assemblies in the filesystem.
    /// Default is "MazeRunner.*.dll". Setting this to empty is equivalent to setting it to "*" and will match all files.
    /// </summary>
    public string WhitelistedAssembliesFilesGlobPattern { get; set; } = "MazeRunner.*.dll";
    
    /// <summary>
    /// Specifies the semicolon-separated the blacklist-glob-patterns (case-insensitive by default) to use when scanning for assemblies in the filesystem.
    /// Setting this to empty is equivalent to completely disabling blacklisting altogether
    /// </summary>
    public string BlacklistedAssembliesFileGlobPattern { get; set; } = "*test.dll;*tests.dll";


    public EnginesFactoryOptions Validate()
    {
        //if (string.IsNullOrWhiteSpace(AssemblyFileFilterWhitelistPattern)) throw //dont   it can be empty
        //if (string.IsNullOrWhiteSpace(AssemblyFileFilterBlacklistPattern)) throw //dont   it can be empty
        //if (!IsDomainAssembliesScanningEnabled && !IsFilesystemAssemblyScanningEnabled) //dont   there are valid use-cases where we want to disable both
        
        return this;
    }
}