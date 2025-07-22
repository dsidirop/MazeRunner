using System;
using FluentAssertions;
using MazeRunner.Contracts;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Tests.TestArtifacts;

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesFactoryTests.GrandEnginesFactoryTestbed.TrySpawnFromAsyncTestbed;

public partial class Testbed
{
    [Test]
    public void Spawn_ShouldReturnProperEngine_GivenValidEngineName()
    {
        // Arrange
        _ = new MazeRunnerSimpleDepthFirstEngine(Artifacts.Minimal_1X3_S_G); //keep this here to ensure the associated assembly is loaded!
        
        var result = (IMazeRunnerEngine) null;
        var factory = new GrandMazeRunnerEnginesFactory(new()
        {
            IsDomainAssembliesScanningEnabled = true,
            IsFilesystemAssemblyScanningEnabled = true,
        });

        // Act
        var work = new Action(() => result = factory.Spawn(nameof(MazeRunnerSimpleDepthFirstEngine), Artifacts.Minimal_1X3_S_G));

        // Assert
        work.Should().NotThrow();

        result.GetType().Should().Be(typeof(MazeRunnerSimpleDepthFirstEngine));
    }
}
