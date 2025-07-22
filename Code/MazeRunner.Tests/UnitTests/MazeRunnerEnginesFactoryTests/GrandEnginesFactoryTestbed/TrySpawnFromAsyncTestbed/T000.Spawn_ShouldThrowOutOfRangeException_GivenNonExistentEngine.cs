using System;
using FluentAssertions;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Tests.TestArtifacts;

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesFactoryTests.GrandEnginesFactoryTestbed.TrySpawnFromAsyncTestbed;

public partial class Testbed
{
    [Test]
    public void Spawn_ShouldThrowOutOfRangeException_GivenNonExistentEngine()
    {
        // Arrange
        var factory = new GrandMazeRunnerEnginesFactory(new()
        {
            IsDomainAssembliesScanningEnabled = false, // we disable all scanning
            IsFilesystemAssemblyScanningEnabled = false, // to force a dud factory
        });

        // Act
        var work = new Action(() => factory.Spawn("foobar", Artifacts.Minimal_1X3_S_G));

        // Assert
        work.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
}
