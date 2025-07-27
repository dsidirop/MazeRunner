using System;
using System.Linq;
using FluentAssertions;
using MazeRunner.Engines.SimpleMazeRunner;
using MazeRunner.EnginesFactory.Factory;

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesFactoryTests.GrandEnginesFactoryTestbed.EnsureInitializedOnceTestbed;

public partial class Testbed
{
    //@formatter:off                        isDomainAssembliesScanningEnabled     isFilesystemAssemblyScanningEnabled
    [TestCase("GEFT.EIO.SCF.GES.000",       true,                                 false                                )]
    [TestCase("GEFT.EIO.SCF.GES.010",       false,                                true                                 )]
    [TestCase("GEFT.EIO.SCF.GES.020",       true,                                 true                                 )] //@formatter:on
    public void EnsureInitializedOnce_ShouldCreateFactory_GivenEnabledScanning(string testNickName, bool isDomainAssembliesScanningEnabled, bool isFilesystemAssemblyScanningEnabled)
    {
        // Arrange

        var success = false;
        var factory = new GrandMazeRunnersEnginesFactory(new()
        {
            IsDomainAssembliesScanningEnabled = isDomainAssembliesScanningEnabled,
            IsFilesystemAssemblyScanningEnabled = isFilesystemAssemblyScanningEnabled,
        });

        // Act
        var work = new Func<bool>(() => success = factory.EnsureInitializedOnce());

        // Assert
        work.Should().NotThrow();

        success.Should().BeTrue();

        factory.LazyCore.Value
            .MazeRunnerEnginesRegistry
            .Keys
            .Count(x => x == nameof(MazeRunnerSimpleDepthFirstEngine))
            .Should()
            .Be(1);
    }
}