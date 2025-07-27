using System;
using FluentAssertions;
using MazeRunner.EnginesFactory.Contracts;

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesFactoryTests.EnginesFactoryOptionsTests;

public partial class Testbed
{
    [Test]
    public void Validate_ShouldNotThrowInvalidOperationException_GivenCompletelyDisabledScanning()
    {
        // Arrange
        var options = new GrandMazeRunnerEnginesFactoryOptions {IsFilesystemAssemblyScanningEnabled = false, IsDomainAssembliesScanningEnabled = false};

        // Act
        var work = new Action(() => options.Validate());

        // Assert
        work.Should().NotThrow();
    }
}
