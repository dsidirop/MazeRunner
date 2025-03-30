using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using MazeRunner.Cli.Engine;
using MazeRunner.Cli.Enums;
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
    static private readonly Tuple<string, string[], EExitCodes>[] Cases; //must be static

    static ControllerIntegrationTests() //dont move this into [onetimesetup]
    {
        FilepathOfArtifactFiles = new ExpandoObject();
        FilepathOfArtifactFiles.OutputMazeFile = SpawnTempFile();
        FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable = SpawnTempFile(Resources.EIT_Maze004_LabyrinthSolvable);

        Cases =
        [
            new Tuple<string, string[], EExitCodes>("000", ["abc"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("001", ["--help"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("002", ["--help=abc"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("003", ["--listengines"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("004", ["--listengines="], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("004", ["--listengines=abc"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("005", ["--engines"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("006", ["--engines=foobar"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("007", [$"--engines=", "--mazefile=abc"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("008", [$"--engines=all", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("009", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", "--mazefile=abc"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("010", [$"--engines=Aaaaaaaabbbbbbbbbbbccccccccccccc", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("011", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("012", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("013", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10", "--verbose"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("014", [$"--engines={nameof(MazeRunnerSimpleDepthFirstEngine)},{nameof(MazeRunnerDepthFirstAvoidPathfoldingEngine)}", $"--mazefile={U.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("015", [$"--generatemaze", "--width=5", "--height=7", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], EExitCodes.Success),
            new Tuple<string, string[], EExitCodes>("016", [$"--generatemaze", "--width=5", "--height=7", "--walldensity=1.1", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("017", [$"--generatemaze", "--width=0", "--height=7", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("018", [$"--generatemaze", "--width=7", "--height=0", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("019", [$"--generatemaze", "--width=1", "--height=1", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("020", [$"--generatemaze", "--width=-1", "--height=1", "--walldensity=0.3", $"--output={U.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}"], EExitCodes.InvalidCommandLineArguments),
            new Tuple<string, string[], EExitCodes>("021", [], EExitCodes.Success),
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
    [Category("Unit.CliControllerIntegrationTests")]
    public async Task ProcessCliArgsAsync_TestCasesSource_ShouldPass(Tuple<string, string[], EExitCodes> @case)
    {
        // Arrange
        var exitcode = EExitCodes.Success;
        var desiredExitCode = @case.Item3;
        var commandLineParams = @case.Item2;
        var standardError = new StringWriter();
        var standardOutput = new StringWriter();

        // Act
        var action = new Func<Task>(async () => exitcode = await new CliControllerEngine(EnginesFactorySingleton.I, new MazesFactory(), new EnginesTestbench(), standardOutput, standardError).ProcessCliArgsAsync(commandLineParams));

        // Assert
        await action.Should().NotThrowAsync();
        exitcode.Should().Be(desiredExitCode);
    }
}