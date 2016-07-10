using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using FluentAssertions;
using MazeRunner.Mazes;
using MazeRunner.Shared.Maze;
using MazeRunner.SimpleMazeRunner;
using MazeRunner.Tests.Properties;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.IntegrationTests
{
    [TestFixture]
    public class EngineIntegrationTests
    {
        private readonly dynamic _filepathOfArtifactFiles = new ExpandoObject();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _filepathOfArtifactFiles.EITMaze001Diagonal = SpawnTempFile(Resources.EIT_Maze001_Diagonal);
            _filepathOfArtifactFiles.EITMaze002GTrap = SpawnTempFile(Resources.EIT_Maze002_GTrap);
            _filepathOfArtifactFiles.EITMaze003GEscape = SpawnTempFile(Resources.EIT_Maze003_GEscape);
            _filepathOfArtifactFiles.EITMaze004LabyrinthSolvable = SpawnTempFile(Resources.EIT_Maze004_LabyrinthSolvable);
            _filepathOfArtifactFiles.EITMaze005LabyrinthUnsolvable = SpawnTempFile(Resources.EIT_Maze005_LabyrinthUnsolvable);
        }

        static private string SpawnTempFile(string contents)
        {
            var tmpfile = Path.GetTempFileName();
            File.WriteAllText(tmpfile, contents);
            return tmpfile;
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
            foreach (var kvp in _filepathOfArtifactFiles)
            {
                File.Delete(kvp.Value);
            }
        }

        [Test]
        [Repeat(30)]
        [Category("Unit.EngineIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_RandomMaze4x4_ShouldFindPath()
        {
            // Arrange
            var maze = MazeFactory.I.Random(4, 4, 0.1); //1 roadblock
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.ShouldNotThrow();
            engine.TrajectoryTip.Should().NotBe(null);
        }

        [Test]
        [Category("Unit.EngineIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_EITMaze001Diagonal_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) MazeFactory.I.FromFile(_filepathOfArtifactFiles.EITMaze001Diagonal);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.ShouldNotThrow();
            engine.InvalidatedSquares.Any().Should().Be(false);
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Category("Unit.EngineIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_EITMaze002GTrap_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) MazeFactory.I.FromFile(_filepathOfArtifactFiles.EITMaze002GTrap);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.ShouldNotThrow();
            engine.InvalidatedSquares.Any().Should().Be(false);
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Category("Unit.EngineIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_EITMaze003GEscape_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) MazeFactory.I.FromFile(_filepathOfArtifactFiles.EITMaze003GEscape);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.ShouldNotThrow();
            engine.InvalidatedSquares.Any().Should().Be(false);
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Repeat(1000)]
        [Category("Unit.EngineIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_EITMaze004LabyrinthSolvable_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) MazeFactory.I.FromFile(_filepathOfArtifactFiles.EITMaze004LabyrinthSolvable);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.ShouldNotThrow();
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Repeat(1000)]
        [Category("Unit.EngineIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_EITMaze005LabyrinthUnsolvable_ShouldFailToFindPath()
        {
            // Arrange
            var maze = (IMaze) MazeFactory.I.FromFile(_filepathOfArtifactFiles.EITMaze005LabyrinthUnsolvable);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.ShouldNotThrow();
            engine.TrajectoryTip.Should().Be(null);
        }
    }
}