﻿using System;
using System.Dynamic;
using System.IO;
using FluentAssertions;
using MazeRunner.Controller.Engine;
using MazeRunner.Engine.SimpleMazeRunner;
using MazeRunner.EnginesFactory;
using MazeRunner.Mazes;
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

        static ControllerIntegrationTests() //dont move this into [onetimesetup]
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
                new Tuple<string, string[], int>("009", new[] {$"--engine={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={TestArtifacts.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}"}, 0),
                new Tuple<string, string[], int>("010", new[] {$"--engine={nameof(MazeRunnerSimpleDepthFirstEngine)}", $"--mazefile={TestArtifacts.Quotify(FilepathOfArtifactFiles.EITMaze004LabyrinthSolvable)}", "--repeat=10"}, 0),
                new Tuple<string, string[], int>("011", new[] {$"--generatemaze", "--width=5", "--height=7", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 0),
                new Tuple<string, string[], int>("012", new[] {$"--generatemaze", "--width=5", "--height=7", "--walldensity=1.1", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("013", new[] {$"--generatemaze", "--width=0", "--height=7", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("014", new[] {$"--generatemaze", "--width=7", "--height=0", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("015", new[] {$"--generatemaze", "--width=1", "--height=1", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("016", new[] {$"--generatemaze", "--width=-1", "--height=1", "--walldensity=0.3", $"--output={TestArtifacts.Quotify(FilepathOfArtifactFiles.OutputMazeFile)}" }, 2),
                new Tuple<string, string[], int>("017", new string[] { }, 0)
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
        [TestCaseSource(nameof(Cases))]
        [Category("Unit.ControllerIntegrationTests")]
        public void ControllerIntegrationTests_Help_ShouldPass(Tuple<string, string[], int> @case)
        {
            // Arrange
            var exitcode = 0;
            var standardError = new StringWriter();
            var standardOutput = new StringWriter();
            var desiredExitCode = @case.Item3;
            var commandLineParams = @case.Item2;

            // Act
            var action = new Action(() => exitcode = new ControllerEngine(EnginesFactorySingleton.I, MazesFactorySingleton.I, standardOutput, standardError).Run(commandLineParams));

            // Assert
            action.ShouldNotThrow();
            exitcode.Should().Be(desiredExitCode);
        }
    }
}