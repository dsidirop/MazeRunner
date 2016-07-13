using System;
using System.Drawing;
using System.Dynamic;
using System.IO;
using FluentAssertions;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
using MazeRunner.Tests.Properties;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.UnitTests.MazeFactoryTests
{
    [TestFixture]
    public class MazeFactoryTests
    {
        private readonly dynamic _filepathOfArtifactFiles = new ExpandoObject();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _filepathOfArtifactFiles.Empty = SpawnTempFile(Resources.MFT_Empty);
            _filepathOfArtifactFiles.Valid3x4 = SpawnTempFile(Resources.MFT_Valid_3x4);
            _filepathOfArtifactFiles.JaggedMaze = SpawnTempFile(Resources.MFT_JaggedMaze);
            _filepathOfArtifactFiles.NoExitpoint = SpawnTempFile(Resources.MFT_NoExitpoint);
            _filepathOfArtifactFiles.MinValid1x2 = SpawnTempFile(Resources.MFT_MinValid_1x2);
            _filepathOfArtifactFiles.MinValid2x1 = SpawnTempFile(Resources.MFT_MinValid_2x1);
            _filepathOfArtifactFiles.NoEntrypoint = SpawnTempFile(Resources.MFT_NoEntrypoint);
            _filepathOfArtifactFiles.InvalidChars = SpawnTempFile(Resources.MFT_InvalidChars);
            _filepathOfArtifactFiles.StrayNewLines = SpawnTempFile(Resources.MFT_StrayNewLines);
            _filepathOfArtifactFiles.InvalidDoubleExitPoints = SpawnTempFile(Resources.MFT_Invalid_DoubleExitPoint);
            _filepathOfArtifactFiles.InvalidDoubleEntryPoints = SpawnTempFile(Resources.MFT_Invalid_DoubleEntryPoint);
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
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileInvalidPathSuppressExceptions_SuppressExceptions_ShouldNotThrowExceptions()
        {
            // Arrange
            var dubiouspath = "/something/that/doesnt/exist";

            // Act
            var action = new Action(() => new MazesFactory().FromFile(dubiouspath, suppressExceptions: true));

            // Assert
            action.ShouldNotThrow();
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileInvalidPathBubbleExceptions_BubbleExceptions_ShouldThrowDirectoryNotFoundException()
        {
            // Arrange
            var dubiouspath = "/something/that/doesnt/exist";

            // Act
            var action = new Action(() => new MazesFactory().FromFile(dubiouspath, suppressExceptions: false));

            // Assert
            action.ShouldThrow<DirectoryNotFoundException>();
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileInvalidCharsInFile_BubbleExceptions_ShouldThrowInvalidDataException()
        {
            // Arrange
            
            // Act
            var action = new Action(() => { new MazesFactory().FromFile(_filepathOfArtifactFiles.InvalidChars, suppressExceptions: false); });

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("Invalid character ! at line 1 column 4");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileInvalidCharsStrayNewLines_BubbleExceptions_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new MazesFactory().FromFile(_filepathOfArtifactFiles.StrayNewLines, suppressExceptions: false); });

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("Line 2 is empty (only the very last line is allowed to be empty)");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileEmpty_BubbleExceptions_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new MazesFactory().FromFile(_filepathOfArtifactFiles.Empty, suppressExceptions: false); });

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("Empty");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileNoEntrypoint_BubbleExceptions_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new MazesFactory().FromFile(_filepathOfArtifactFiles.NoEntrypoint, suppressExceptions: false); });

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("No entrypoint specified");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileNoExitpoint_BubbleExceptions_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new MazesFactory().FromFile(_filepathOfArtifactFiles.NoExitpoint, suppressExceptions: false); });

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("No exitpoint specified");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileJaggedMaze_BubbleExceptions_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => { new MazesFactory().FromFile(_filepathOfArtifactFiles.JaggedMaze, suppressExceptions: false); });

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("Line 2 has different number of columns (7) than the first line (which has 8)");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileMinValid1x2_BubbleExceptions_ShouldReturnValidMaze()
        {
            // Arrange
            var result = (IMaze) null;

            // Act
            var action = new Action(() => { result = new MazesFactory().FromFile(_filepathOfArtifactFiles.MinValid1x2, suppressExceptions: false); });
            
            // Assert
            action.ShouldNotThrow();
            result.Size.Should().Be(new Size(width: 2, height: 1));
            result.Exitpoint.Should().Be(new Point(x: 1, y: 0));
            result.Entrypoint.Should().Be(new Point(x: 0, y: 0));
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_FromFileValid3x4_BubbleExceptions_ShouldReturnValidMaze()
        {
            // Arrange
            var result = (IMaze) null;
            var desired = (File.ReadAllText(_filepathOfArtifactFiles.Valid3x4).Trim() as string).NormalizeNewlines("\r\n");

            // Act
            var action = new Action(() => { result = new MazesFactory().FromFile(_filepathOfArtifactFiles.Valid3x4, suppressExceptions: false); });

            // Assert
            action.ShouldNotThrow();
            result.ToAsciiMap(linesSeparator: Utilities.nl).Should().Be(desired); //0
        }
        //0 We force the lineseparator used by toasciimap to be the windows style newline in order to have this test pass on unix platforms
        //  If we omit this sort of enforcement then the line seperator that will be used will be \n instead of \r\n which will cause the comparison to fail
        //  Also bare in mind that git likes to normalize newlines into unixstyle \n newlines which also causes problems all by its own

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_InvalidDoubleEntryPoints_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => new MazesFactory().FromFile(_filepathOfArtifactFiles.InvalidDoubleEntryPoints, suppressExceptions: false));

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("Maze has two Entry points");
        }

        [Test]
        [Category("Unit.MazeFactory")]
        public void MazeFactory_InvalidDoubleExitPoints_ShouldThrowInvalidDataException()
        {
            // Arrange

            // Act
            var action = new Action(() => new MazesFactory().FromFile(_filepathOfArtifactFiles.InvalidDoubleExitPoints, suppressExceptions: false));

            // Assert
            action.ShouldThrow<InvalidDataException>().WithMessage("Maze has two exit points");
        }
    }
}
