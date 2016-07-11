using System;
using System.Dynamic;
using System.IO;
using FluentAssertions;
using MazeRunner.Controller.Bootstrapping;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.Tests.Artifacts;
using MazeRunner.Tests.Properties;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.IntegrationTests
{
    [TestFixture]
    public class ControllerIntegrationTests
    {
        static private readonly Tuple<string, string[], int>[] Cases; //must be static
        static private readonly dynamic FilepathOfArtifactFiles = new ExpandoObject();

        static ControllerIntegrationTests()
        {
            FilepathOfArtifactFiles.OutputMazeFile = SpawnTempFile();
            FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable = SpawnTempFile(Resources.EIT_Maze004_LabyrinthSolvable);

            Cases = new[]
            {
                new Tuple<string, string[], int>("000", new[] {"abc"}, 1),
                new Tuple<string, string[], int>("001", new[] {"--help"}, 0),
                new Tuple<string, string[], int>("002", new[] {"--help=abc"}, 1),
                new Tuple<string, string[], int>("003", new[] {"--listengines"}, 0),
                new Tuple<string, string[], int>("004", new[] {"--listengines=abc"}, 1),
                new Tuple<string, string[], int>("005", new[] {"--engine"}, 1),
                new Tuple<string, string[], int>("006", new[] {"--engine=foobar"}, 1),
                new Tuple<string, string[], int>("007", new[] {$"--engine={nameof(MazeRunnerSimpleDepthFirstEngine)}", "--mazefile=abc"}, 3),
                new Tuple<string, string[], int>("008", new[] {$"--engine=Aaaaaaaabbbbbbbbbbbccccccccccccc", $"--mazefile={TestArtifacts.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"}, 3),
                new Tuple<string, string[], int>("009", new[] {$"--engine={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={TestArtifacts.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"}, 0), //fix
                new Tuple<string, string[], int>("010", new[] {$"--engine={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={TestArtifacts.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"}, 0), //fix
                new Tuple<string, string[], int>("011", new[] {$"--generatemaze", "--width=5", "--height=7", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 0), //fix
                new Tuple<string, string[], int>("012", new[] {$"--generatemaze", "--width=5", "--height=7", "--walldensity=1.1", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("013", new[] {$"--generatemaze", "--width=0", "--height=7", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("014", new[] {$"--generatemaze", "--width=7", "--height=0", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("015", new[] {$"--generatemaze", "--width=1", "--height=1", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2)
            };
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
        [Category("Unit.ControllerIntegrationTests")]
        public void CommandLineTests_PrintUsageMessage_ShouldPass()
        {
            // Arrange
            var exitcode = 0;

            // Act
            var action = new Action(() => exitcode = Bootstrapper.Run(new string[] {}));

            // Assert
            action.ShouldNotThrow();
            exitcode.Should().Be(0);
        }

        [Test]
        [TestCaseSource(nameof(Cases))]
        [Category("Unit.ControllerIntegrationTests")]
        public void CommandLineTests_Help_ShouldPass(Tuple<string, string[], int> @case)
        {
            // Arrange
            var exitcode = 0;

            // Act
            var action = new Action(() => exitcode = Bootstrapper.Run(@case.Item2));

            // Assert
            action.ShouldNotThrow();
            exitcode.Should().Be(@case.Item3);
        }
    }
}
