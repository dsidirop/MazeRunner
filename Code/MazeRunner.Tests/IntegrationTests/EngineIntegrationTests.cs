using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using FluentAssertions;
using MazeRunner.Contracts;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.Mazes;
using MazeRunner.Tests.Properties;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.IntegrationTests
{
    [TestFixture(typeof(MazeRunnerSimpleDepthFirstEngine))]
    [TestFixture(typeof(MazeRunnerDepthFirstAvoidPathfoldingEngine))]
    public class EngineIntegrationTests<TEngine> where TEngine : IMazeRunnerEngine
    {
        static private IMazeRunnerEngine SpawnEngine(IMaze maze) => (TEngine) Activator.CreateInstance(typeof(TEngine), maze);

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
        public void EngineIntegrationTests_RandomMaze4x4_ShouldFindPath()
        {
            // Arrange
            var maze = new MazesFactory().Random(4, 4, 0.1); //1 roadblock
            var engine = SpawnEngine(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.Should().NotThrow();
            engine.TrajectoryTip.Should().NotBe(null);
        }

        [Test]
        [Category("Unit.EngineIntegrationTests")]
        public void EngineIntegrationTests_EITMaze001Diagonal_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) new MazesFactory().FromFile(_filepathOfArtifactFiles.EITMaze001Diagonal);
            var engine = SpawnEngine(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.Should().NotThrow();
            engine.InvalidatedSquares.Any().Should().Be(false);
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Category("Unit.EngineIntegrationTests")]
        public void EngineIntegrationTests_EITMaze002GTrap_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) new MazesFactory().FromFile(_filepathOfArtifactFiles.EITMaze002GTrap);
            var engine = SpawnEngine(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.Should().NotThrow();
            engine.InvalidatedSquares.Any().Should().Be(false);
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Category("Unit.EngineIntegrationTests")]
        public void EngineIntegrationTests_EITMaze003GEscape_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) new MazesFactory().FromFile(_filepathOfArtifactFiles.EITMaze003GEscape);
            var engine = SpawnEngine(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.Should().NotThrow();
            engine.InvalidatedSquares.Any().Should().Be(false);
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Repeat(1000)]
        [Category("Unit.EngineIntegrationTests")]
        public void EngineIntegrationTests_EITMaze004LabyrinthSolvable_ShouldFindPath()
        {
            // Arrange
            var maze = (IMaze) new MazesFactory().FromFile(_filepathOfArtifactFiles.EITMaze004LabyrinthSolvable);
            var engine = SpawnEngine(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.Should().NotThrow();
            engine.TrajectoryTip.Should().Be(maze.Exitpoint);
        }

        [Test]
        [Repeat(1000)]
        [Category("Unit.EngineIntegrationTests")]
        public void EngineIntegrationTests_EITMaze005LabyrinthUnsolvable_ShouldFailToFindPath()
        {
            // Arrange
            var maze = (IMaze) new MazesFactory().FromFile(_filepathOfArtifactFiles.EITMaze005LabyrinthUnsolvable);
            var engine = SpawnEngine(maze);

            // Act
            var action = new Action(() => engine.Run());

            // Assert
            action.Should().NotThrow();
            engine.TrajectoryTip.Should().Be(null);
        }
    }
}