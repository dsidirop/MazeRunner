using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Maze;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

// notice that this testbed project runs in x86 mode  this was done in order to workaround an apparent shortcoming plaguing certain versions resharper
// which causes unittest debugging to hang upon launching the debugger in order to debug any of the tests   you may switch this project back to anycpu
// mode if your version of resharper doesnt suffer from the aforementioned glitch

namespace MazeRunner.Tests.MazeTests
{
    [TestFixture]
    public class MazeTests
    {
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
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeWidth_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(0, 10), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeHeight_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(10, 0), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeOneNegativeDimensionWidth_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(-1, 2), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeOneNegativeDimensionHeight_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(2, -1), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeBothDimensionsNegative_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(-1, -1), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeOnlyOneSquare_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(1, 1), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidSizeTooBig_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(int.MaxValue, int.MaxValue), Point.Empty, new Point(1, 1), new HashSet<Point>()); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidRoadblockCoords_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new Maze(new Size(2, 1), Point.Empty, new Point(x: 1, y: 0), new HashSet<Point> {new Point(x: 2, y: 0)}); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("roadblocks");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidRoadblockEntryPointOutOfBounds_ThrowsArgumentException()
        {
            // Arrange
            var entrypoint = new Point(-1, -1);
            var roadblocks = new HashSet<Point>();

            // Act
            var action = new Action(() => { new Maze(new Size(2, 1), entrypoint, new Point(x: 1, y: 0), roadblocks); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("entrypoint");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidRoadblockExitPointOutOfBounds_ThrowsArgumentException()
        {
            // Arrange
            var exitpoint = new Point(-1, -1);
            var roadblocks = new HashSet<Point>();

            // Act
            var action = new Action(() => { new Maze(new Size(2, 1), Point.Empty, exitpoint, roadblocks); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("exitpoint");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidRoadblockConflictWithEntryPoint_ThrowsArgumentException()
        {
            // Arrange
            var entrypoint = Point.Empty;
            var roadblocks = new HashSet<Point> {entrypoint};

            // Act
            var action = new Action(() => { new Maze(new Size(2, 1), entrypoint, new Point(x: 1, y: 0), roadblocks); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("entrypoint");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_InvalidRoadblockConflictWithExitPoint_ThrowsArgumentException()
        {
            // Arrange
            var exitpoint = new Point(x: 1, y: 0);
            var roadblocks = new HashSet<Point> { exitpoint };

            // Act
            var action = new Action(() => { new Maze(new Size(2, 1), Point.Empty, exitpoint, roadblocks); });

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("exitpoint");
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_Minimal1x2_ShouldPass()
        {
            // Arrange
            var size = new Size(2, 1);
            var exitpoint = new Point(x: 1, y: 0);
            var entrypoint = Point.Empty;

            // Act
            var maze = new Maze(size, entrypoint, exitpoint, new HashSet<Point>()) as IMaze;

            // Assert
            maze.Size.Should().Be(size);
            maze.Exitpoint.Should().Be(exitpoint);
            maze.Entrypoint.Should().Be(entrypoint);
            maze.HitTest(maze.Exitpoint).ShouldBeEquivalentTo(MazeHitTestEnum.Exitpoint);
            maze.HitTest(maze.Entrypoint).ShouldBeEquivalentTo(MazeHitTestEnum.Entrypoint);
            maze.HitTest(new Point(-1, -1)).ShouldBeEquivalentTo(MazeHitTestEnum.Roadblock);
        }

        [Test]
        [Category("Unit.Maze")]
        public void Maze_Maze10x10_ShouldPass()
        {
            // Arrange
            var size = new Size(width: 6, height: 6);
            var exitpoint = new Point(x: 4, y: 4);
            var entrypoint = new Point(x: 2, y: 2);
            var roadblocks = new HashSet<Point> { new Point(x: 0, y: 0), new Point(x: 1, y: 1), new Point(x: 3, y: 3) };

            // Act
            var maze = new Maze(size, entrypoint, exitpoint, roadblocks) as IMaze;

            // Assert
            maze.Size.Should().Be(size);

            var allCoords =
                from x in Enumerable.Range(0, count: size.Width)
                from y in Enumerable.Range(0, count: size.Height)
                select new Point(x, y);
            allCoords.ForEach(x =>
            {
                var expectedHittestResult = MazeHitTestEnum.Free;
                if (x == maze.Exitpoint)
                {
                    expectedHittestResult = MazeHitTestEnum.Exitpoint;
                }
                else if (x == maze.Entrypoint)
                {
                    expectedHittestResult = MazeHitTestEnum.Entrypoint;
                }
                else if (roadblocks.Contains(x))
                {
                    expectedHittestResult = MazeHitTestEnum.Roadblock;
                }

                maze.HitTest(x).ShouldBeEquivalentTo(expectedHittestResult, "Square ({0}) was expected to hittest as '{1}' but was found to hittest as '{2}'", x, expectedHittestResult, maze.HitTest(x));
            });
        }
    }
}
