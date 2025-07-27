using System;
using FluentAssertions;
using MazeRunner.EnginesFactory.Factory;

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesFactoryTests.GrandEnginesFactoryTestbed.EnsureInitializedOnceTestbed;

public partial class Testbed
{
    [Test]
    public void EnsureInitializedOnce_ShouldCreateDudFactory_GivenCompletelyDisabledScanning()
    {
        // Arrange
        var success = false;
        var factory = new GrandMazeRunnersEnginesFactory(new()
        {
            IsDomainAssembliesScanningEnabled = false,
            IsFilesystemAssemblyScanningEnabled = false,
        });

        // Act
        var work = new Func<bool>(() => success = factory.EnsureInitializedOnce());

        // Assert
        work.Should().NotThrow();

        success.Should().BeTrue();
    }
}
