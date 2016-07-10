using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using FluentAssertions;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.SimpleMazeRunner;
using MazeRunner.Tests.Properties;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.IntegrationTests
{
    [TestFixture]
    public class MazeIntegrationTests
    {
        private readonly dynamic _filepathOfArtifactFiles = new ExpandoObject();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _filepathOfArtifactFiles.EITMaze004LabyrinthSolvable = SpawnTempFile(Resources.EIT_Maze004_LabyrinthSolvable);
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
        [Category("Unit.MazeIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_PrintSolvedRandomMaze4x4_ShouldFindPath()
        {
            // Arrange
            var maze = MazeFactory.I.Random(4, 4, 0.1);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() =>
            {
                engine.Run();
                maze.ToAsciiMap(p => engine.Trajectory.Contains(p) ? '*' : (engine.InvalidatedSquares.Contains(p) ? (char?) '#' : null));
            });

            // Assert
            action.ShouldNotThrow();
            engine.TrajectoryTip.Should().NotBe(null);
        }
        // Console.WriteLine(printedsolution + $@"{Utilities.nl}------------");

        [Test]
        [Repeat(30)]
        [Category("Unit.MazeIntegrationTests")]
        public void MazeRunnerEngineDepthFirstPolicy_PrintSolvedRandomMaze10x10_ShouldFindPath()
        {
            // Arrange
            var maze = MazeFactory.I.Random(10, 10, 0.1);
            var engine = new MazeRunnerEngineDepthFirstAvoidPathfoldingPolicy(maze);

            // Act
            var action = new Action(() =>
            {
                engine.Run();
                maze.ToAsciiMap(p => engine.Trajectory.Contains(p) ? '*' : (engine.InvalidatedSquares.Contains(p) ? (char?)'#' : null));
            });

            // Assert
            action.ShouldNotThrow();
        }
        // Console.WriteLine(printedsolution + $@"{Utilities.nl}------------");
    }
}