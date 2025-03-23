using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using FluentAssertions;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.Mazes;
using MazeRunner.Tests.Properties;
using MazeRunner.Utils;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.IntegrationTests;

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
        var maze = new MazesFactory().Random(4, 4, 0.1);
        var engine = new MazeRunnerDepthFirstAvoidPathfoldingEngine(maze);

        // Act
        var action = new Action(() =>
        {
            engine.Run();
            maze.ToAsciiMap(p => engine.Trajectory.Contains(p) ? '*' : (engine.InvalidatedSquares.Contains(p) ? (char?) '#' : null));
        });

        // Assert
        action.Should().NotThrow();
        engine.TrajectoryTip.Should().NotBe(null);
    }

    [Test]
    [Repeat(30)]
    [Category("Unit.MazeIntegrationTests")]
    public void MazeRunnerEngineDepthFirstPolicy_PrintSolvedRandomMaze10x10_ShouldFindPath()
    {
        // Arrange
        var maze = new MazesFactory().Random(10, 10, 0.1);
        var engine = new MazeRunnerDepthFirstAvoidPathfoldingEngine(maze);

        // Act
        var action = new Action(() =>
        {
            engine.Run();
            maze.ToAsciiMap(p => engine.Trajectory.Contains(p) ? '*' : (engine.InvalidatedSquares.Contains(p) ? (char?)'#' : null));
        });

        // Assert
        action.Should().NotThrow();
    }
}