using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using MazeRunner.Contracts;
using MazeRunner.Engine.SimpleMazeRunner;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesTests;

[TestFixture(typeof(MazeRunnerSimpleDepthFirstEngine))]
[TestFixture(typeof(MazeRunnerDepthFirstAvoidPathfoldingEngine))]
public class MazeRunnerEnginesTests<TEngine> where TEngine : IMazeRunnerEngine
{
    static private IMazeRunnerEngine SpawnEngine(IMaze maze) => (TEngine) Activator.CreateInstance(typeof (TEngine), maze);

    [OneTimeSetUp]
    public void TestFixtureSetUp()
    {
    }

    [SetUp]
    public void SetUp()
    {
    }

    [TearDown]
    public void TearDown()
    {
    }

    [OneTimeTearDown]
    public void TestFixtureTearDown()
    {
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_NullArgument_ShouldThrowNullReferenceException()
    {
        // Arrange

        // Act
        var action = new Action(() => SpawnEngine(maze: null));

        // Assert
        action.Should().Throw<TargetInvocationException>().WithInnerException<ArgumentNullException>("Value cannot be null.Parameter name: maze");
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_Minimal1x2_ShouldFindPath() //SG
    {
        // Arrange
        var maze = TestArtifacts.Artifacts.Minimal_1X2_SG;
        var runner = SpawnEngine(maze);
        using var eventsMonitor = runner.Monitor();

        // Act
        var action = new Action(() => runner.Run());

        // Assert
            
        action.Should().NotThrow();
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.Concluded)).Count().Should().Be(1);
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.StateChanged));

        runner.Trajectory.Should().BeEquivalentTo([maze.Entrypoint, maze.Exitpoint]);
        runner.TrajectoryTip.Should().Be(maze.Exitpoint);
        runner.TrajectoryLength.Should().Be(2);
        runner.InvalidatedSquares.Any().Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_ResetEngine_ShouldReset()
    {
        // Arrange
        var maze = TestArtifacts.Artifacts.Minimal_1X2_SG;
        var runner = SpawnEngine(maze);
        using var eventsMonitor = runner.Monitor();

        // Act
        var action = new Action(() => runner.Run().Reset());

        // Assert
        action.Should().NotThrow();
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.Concluded)).Count().Should().Be(1);
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.StateChanged));

        runner.Trajectory.Any().Should().Be(false);
        runner.TrajectoryTip.Should().Be(null);
        runner.TrajectoryLength.Should().Be(0);
        runner.InvalidatedSquares.Any().Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_Minimal1x3Horizontal_ShouldFindPath() //S_G horizontal
    {
        // Arrange
        var maze = TestArtifacts.Artifacts.Minimal_1X3_S_G;
        var runner = SpawnEngine(maze);
        using var eventsMonitor = runner.Monitor();

        // Act
        var action = new Action(() => runner.Run());

        // Assert
        action.Should().NotThrow();
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.Concluded)).Count().Should().Be(1);
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.StateChanged)).Count().Should().Be(3);

        runner.Trajectory.Should().BeEquivalentTo([maze.Entrypoint, new Point(x: 1, y: 0), maze.Exitpoint]);
        runner.TrajectoryTip.Should().Be(maze.Exitpoint);
        runner.TrajectoryLength.Should().Be(3);
        runner.InvalidatedSquares.Any().Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_Minimal1x3Horizontal_ShouldFailToFindPath() //SXG horizontal
    {
        // Arrange
        var maze = TestArtifacts.Artifacts.Minimal_1X3_SXG;
        var runner = SpawnEngine(maze);
        using var eventsMonitor = runner.Monitor();

        // Act
        var action = new Action(() => runner.Run());

        // Assert
        action.Should().NotThrow();
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.Concluded)).Count().Should().Be(1);
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.StateChanged)).Count().Should().Be(2);

        runner.Trajectory.Any().Should().Be(false);
        runner.TrajectoryTip.Should().Be(null);
        runner.TrajectoryLength.Should().Be(0);
        runner.InvalidatedSquares.Should().BeEquivalentTo([maze.Entrypoint]);
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_Minimal1x3Vertical_ShouldFindPath() //S_G vertical
    {
        // Arrange
        var maze = TestArtifacts.Artifacts.Minimal_3X1_S_G;
        var runner = SpawnEngine(maze);
        using var eventsMonitor = runner.Monitor();

        // Act
        var action = new Action(() => runner.Run());

        // Assert
        action.Should().NotThrow();
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.Concluded)).Count().Should().Be(1);
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.StateChanged)).Count().Should().Be(3);

        runner.TrajectoryTip.Should().Be(maze.Exitpoint);
        runner.InvalidatedSquares.Any().Should().Be(false);
        runner.Trajectory.Should().BeEquivalentTo([maze.Entrypoint, new Point(x: 0, y: 1), maze.Exitpoint]);
    }

    [Test]
    [Category("Unit.MazeRunnerEnginesTests")]
    public void MazeRunnerEnginesTests_Minimal1x3Vertical_ShouldNotFindPath() //SXG vertical
    {
        // Arrange
        var maze = TestArtifacts.Artifacts.Minimal_3X1_SXG;
        var runner = SpawnEngine(maze);
        using var eventsMonitor = runner.Monitor();

        // Act
        var action = new Action(() => runner.Run());

        // Assert
        action.Should().NotThrow();
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.Concluded)).Count().Should().Be(1);
        eventsMonitor.Should().Raise(nameof(IMazeRunnerEngine.StateChanged)).Count().Should().Be(2);

        runner.TrajectoryTip.Should().Be(null);
        runner.InvalidatedSquares.Should().BeEquivalentTo([maze.Entrypoint]);
        runner.Trajectory.Any().Should().Be(false);
    }
}