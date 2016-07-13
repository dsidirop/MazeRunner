using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.Mazes;
using MazeRunner.Shared.Interfaces;
//using System.Dynamic;
//using System.IO;
//using FluentAssertions;
//using MazeRunner.Mazes;
//using MazeRunner.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.MSTests.UnitTests.MazeFactoryTests
{
    [TestClass]
    public class MazeFactoryTests
    {
        // static private readonly dynamic _filepathOfArtifactFiles = new ExpandoObject();

        [AssemblyInitialize]
        static public void AssemblyInit(TestContext context)
        {
            // _filepathOfArtifactFiles.Empty = SpawnTempFile(Resources.MFT_Empty);
        }

        [ClassInitialize]
        static public void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [ClassCleanup]
        static public void ClassCleanup()
        {
        }

        [AssemblyCleanup]
        static public void AssemblyCleanup()
        {
            //foreach (var kvp in _filepathOfArtifactFiles)
            //{
            //    File.Delete(kvp.Value);
            //}
        }

        //static private string SpawnTempFile(string contents)
        //{
        //    var tmpfile = Path.GetTempFileName();
        //    File.WriteAllText(tmpfile, contents);
        //    return tmpfile;
        //}

        [TestMethod]
        [TestCategory("Unit.MazeFactory")]
        public void MazeFactory_FromFileInvalidPathSuppressExceptions_SuppressExceptions_ShouldNotThrowExceptions()
        {
            // Arrange
            var dubiouspath = "/something/that/doesnt/exist";

            // Act
            // var action = new Action(() => new MazesFactory().FromFile(dubiouspath, suppressExceptions: true));

            // Assert
            // action.ShouldNotThrow();
        }

        [TestMethod]
        [TestCategory("Unit.Maze")]
        public void Maze_InvalidSizeWidth_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var action = new Action(Maze_InvalidSizeWidth_ThrowsArgumentException_helper);

            // Assert
            action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        private void Maze_InvalidSizeWidth_ThrowsArgumentException_helper()
        {
            new Maze(new Size(0, 10), Point.Empty, new Point(1, 1), new HashSet<Point>());
        }

        [TestMethod]
        [TestCategory("Unit.Maze")]
        public void MazeRunnerEnginesTests_Minimal1x3Horizontal_ShouldFindPath() //S_G horizontal
        {
            // Arrange
            var maze = TestArtifacts.Artifacts.Minimal_1X3_S_G;
            var runner = new MazeRunnerDepthFirstAvoidPathfoldingEngine(maze);

            // Act
            runner.MonitorEvents();
            runner.Run();

            // Assert
            runner.ShouldRaise(nameof(IMazeRunnerEngine.Concluded));
            runner.ShouldRaise(nameof(IMazeRunnerEngine.StateChanged));

            runner.Trajectory.ShouldAllBeEquivalentTo(new[] { maze.Entrypoint, new Point(x: 1, y: 0), maze.Exitpoint });
            runner.TrajectoryTip.Should().Be(maze.Exitpoint);
            runner.TrajectoryLength.Should().Be(3);
            runner.InvalidatedSquares.Any().Should().Be(false);
        }
    }
}
