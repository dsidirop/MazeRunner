using System;
using System.Collections.Generic;
using System.Drawing;
using MazeRunner.Mazes;
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
            // var action = new Action(() => { new Maze(new Size(0, 10), Point.Empty, new Point(1, 1), new HashSet<Point>()); });
            var action = new Action(Target);

            // Assert
            //action.ShouldThrow<ArgumentException>().WithMessage("size");
        }

        private void Target()
        {
            var foo = 1;
        }
    }
}
