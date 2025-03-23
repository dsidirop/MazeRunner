using System;
using System.Dynamic;
using System.IO;
using FluentAssertions;
using MazeRunner.Cli.Engine;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Factory;
using MazeRunner.Mazes;
using MazeRunner.Tests.Properties;
using MazeRunner.Utils;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.IntegrationTests;

[TestFixture]
public class ControllerIntegrationTests
{
    static private readonly dynamic FilepathOfArtifactFiles;
    static private readonly Tuple<string, string[], int>[] Cases; //must be static

    static ControllerIntegrationTests() //dont move this into [onetimesetup]
    {
        FilepathOfArtifactFiles = new ExpandoObject();
        FilepathOfArtifactFiles.OutputMazeFile = SpawnTempFile();
        FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable = SpawnTempFile(Resources.EIT_Maze004_LabyrinthSolvable);

        Cases =
        [
            new Tuple<string, string[], int>("000", ["abc"], 1),
            new Tuple<string, string[], int>("001", ["--help"], 0),
            new Tuple<string, string[], int>("002", ["--help=abc"], 1),
            new Tuple<string, string[], int>("003", ["--listengines"], 0),
            new Tuple<string, string[], int>("004", ["--listengines="], 1),
            new Tuple<string, string[], int>("004", ["--listengines=abc"], 1),
            new Tuple<string, string[], int>("005", ["--engines"], 1),
            new Tuple<string, string[], int>("006", ["--engines=foobar"], 1),
            new Tuple<string, string[], int>("007", [$"--engines=", "--mazefile=abc"], 1),
            new Tuple<string, string[], int>("010", [$"--engines=all", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"], 0),
            new Tuple<string, string[], int>("007", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", "--mazefile=abc"], 3),
            new Tuple<string, string[], int>("008", [$"--engines=Aaaaaaaabbbbbbbbbbbccccccccccccc", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"], 3),
            new Tuple<string, string[], int>("009", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"], 0),
            new Tuple<string, string[], int>("010", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"], 0),
            new Tuple<string, string[], int>("011", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10", "--verbose"], 0),
            new Tuple<string, string[], int>("010", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)},{nameof(MazeRunnerDepthFirstAvoidPathfoldingEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"], 0),
            new Tuple<string, string[], int>("012", [$"--generatemaze", "--width=5", "--height=7", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], 0),
            new Tuple<string, string[], int>("013", [$"--generatemaze", "--width=5", "--height=7", "--walldensity=1.1", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], 2),
            new Tuple<string, string[], int>("014", [$"--generatemaze", "--width=0", "--height=7", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], 2),
            new Tuple<string, string[], int>("015", [$"--generatemaze", "--width=7", "--height=0", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], 2),
            new Tuple<string, string[], int>("016", [$"--generatemaze", "--width=1", "--height=1", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], 2),
            new Tuple<string, string[], int>("017", [$"--generatemaze", "--width=-1", "--height=1", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], 2),
            new Tuple<string, string[], int>("018", [], 0)
        ];
    }

    [OneTimeSetUp]
    public void TestFixtureSetUp()
    {

    }

    static private string SpawnTempFile(string contents = "")
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
        foreach (var kvp in FilepathOfArtifactFiles)
        {
            File.Delete(kvp.Value);
        }
    }

    [Test]
    [TestCaseSource(nameof(Cases))]
    [Category("Unit.ControllerIntegrationTests")]
    public void ControllerIntegrationTests_TestCasesSource_ShouldPass(Tuple<string, string[], int> @case)
    {
        // Arrange
        var exitcode = 0;
        var standardError = new StringWriter();
        var standardOutput = new StringWriter();
        var desiredExitCode = @case.Item3;
        var commandLineParams = @case.Item2;

        // Act
        var action = new Action(() => exitcode = new ControllerEngine(EnginesFactorySingleton.I, new MazesFactory(), new EnginesTestbench(), standardOutput, standardError).Run(commandLineParams));

        // Assert
        action.Should().NotThrow();
        exitcode.Should().Be(desiredExitCode);
    }
}