using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using MazeRunner.Shared.Engine;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Maze;
using MazeRunner.SimpleMazeRunner;
using Moq;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.UnitTests.MazeRunnerEnginesTests
{
    [TestFixture(typeof(MazeRunnerEngineSimpleDepthFirstPolicy))]
    [TestFixture(typeof(MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy))]
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
        public void MazeRunnerEngineDepthFirstPolicy_NullArgument_ShouldThrowNullReferenceException()
        {
            // Arrange

            // Act
            var action = new Action(() => SpawnEngine(maze: null));

            // Assert
            action.ShouldThrow<TargetInvocationException>().WithInnerException<ArgumentNullException>("Value cannot be null.Parameter name: maze");
        }

        [Test]
        [Category("Unit.MazeRunnerEnginesTests")]
        public void MazeRunnerEngineDepthFirstPolicy_Minimal1x2_ShouldFindPath() //SG
        {
            // Arrange
            var size = new Size(width: 1, height: 1);
            var exitpoint = new Point(x: 1, y: 0);
            var entrypoint = new Point(x: 0, y: 0);

            var mazemock = new Mock<IMaze>();
            mazemock.Setup(x => x.Size).Returns(size);
            mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
            {
                if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                if (p == entrypoint) return MazeHitTestEnum.Entrypoint;
                return MazeHitTestEnum.Roadblock;
            });
            mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
            mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);

            var runner = SpawnEngine(mazemock.Object);

            // Act
            var action = new Action(() => runner.Run());

            // Assert
            runner.MonitorEvents();
            action.ShouldNotThrow();
            runner.ShouldRaise(nameof(IMazeRunnerEngine.Concluded));
            runner.ShouldRaise(nameof(IMazeRunnerEngine.StateChanged));
            runner.TrajectoryTip.Should().Be(exitpoint);
            runner.InvalidatedSquares.Any().Should().Be(false);
            runner.Trajectory.ShouldAllBeEquivalentTo(new[] {entrypoint, exitpoint});
        }

        [Test]
        [Category("Unit.MazeRunnerEnginesTests")]
        public void MazeRunnerEngineDepthFirstPolicy_Minimal1x3Horizontal_ShouldFindPath() //S_G horizontal
        {
            // Arrange
            var size = new Size(width: 3, height: 1);
            var exitpoint = new Point(x: 2, y: 0);
            var freepoint = new Point(x: 1, y: 0);
            var entrypoint = new Point(x: 0, y: 0);

            var mazemock = new Mock<IMaze>();
            mazemock.Setup(x => x.Size).Returns(size);
            mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
            {
                if (p == freepoint) return MazeHitTestEnum.Free;
                if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                return MazeHitTestEnum.Roadblock;
            });
            mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
            mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);

            var runner = SpawnEngine(mazemock.Object);

            // Act
            var action = new Action(() => runner.Run());

            // Assert
            action.ShouldNotThrow();
            runner.TrajectoryTip.Should().Be(exitpoint);
            runner.InvalidatedSquares.Any().Should().Be(false);
            runner.Trajectory.ShouldAllBeEquivalentTo(new[] {entrypoint, freepoint, exitpoint});
        }

        [Test]
        [Category("Unit.MazeRunnerEnginesTests")]
        public void MazeRunnerEngineDepthFirstPolicy_Minimal1x3Horizontal_ShouldFailToFindPath() //SXG horizontal
        {
            // Arrange
            var size = new Size(width: 3, height: 1);
            var exitpoint = new Point(x: 2, y: 0);
            var entrypoint = new Point(x: 0, y: 0);

            var mazemock = new Mock<IMaze>();
            mazemock.Setup(x => x.Size).Returns(size);
            mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
            {
                if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                return MazeHitTestEnum.Roadblock;
            });
            mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
            mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);

            var runner = SpawnEngine(mazemock.Object);

            // Act
            var action = new Action(() => runner.Run());

            // Assert
            action.ShouldNotThrow();
            runner.TrajectoryTip.Should().Be(null);
            runner.InvalidatedSquares.ShouldBeEquivalentTo(new[] {entrypoint});
            runner.Trajectory.Any().Should().Be(false);
        }

        [Test]
        [Category("Unit.MazeRunnerEnginesTests")]
        public void MazeRunnerEngineDepthFirstPolicy_Minimal1x3Vertical_ShouldFindPath() //S_G vertical
        {
            // Arrange
            var size = new Size(width: 1, height: 3);
            var exitpoint = new Point(x: 0, y: 2);
            var freepoint = new Point(x: 0, y: 1);
            var entrypoint = new Point(x: 0, y: 0);

            var mazemock = new Mock<IMaze>();
            mazemock.Setup(x => x.Size).Returns(size);
            mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
            {
                if (p == freepoint) return MazeHitTestEnum.Free;
                if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                return MazeHitTestEnum.Roadblock;
            });
            mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
            mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);

            var runner = SpawnEngine(mazemock.Object);

            // Act
            var action = new Action(() => runner.Run());

            // Assert
            action.ShouldNotThrow();
            runner.TrajectoryTip.Should().Be(exitpoint);
            runner.InvalidatedSquares.Any().Should().Be(false);
            runner.Trajectory.ShouldAllBeEquivalentTo(new[] { entrypoint, freepoint, exitpoint });
        }

        [Test]
        [Category("Unit.MazeRunnerEnginesTests")]
        public void MazeRunnerEngineDepthFirstPolicy_Minimal1x3Vertical_ShouldNotFindPath() //SXG vertical
        {
            // Arrange
            var size = new Size(width: 1, height: 3);
            var exitpoint = new Point(x: 0, y: 2);
            var entrypoint = new Point(x: 0, y: 0);

            var mazemock = new Mock<IMaze>();
            mazemock.Setup(x => x.Size).Returns(size);
            mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
            {
                if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                return MazeHitTestEnum.Roadblock;
            });
            mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
            mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);

            var runner = SpawnEngine(mazemock.Object);

            // Act
            var action = new Action(() => runner.Run());

            // Assert
            action.ShouldNotThrow();
            runner.TrajectoryTip.Should().Be(null);
            runner.InvalidatedSquares.ShouldBeEquivalentTo(new[] {entrypoint});
            runner.Trajectory.Any().Should().Be(false);
        }
    }
}
