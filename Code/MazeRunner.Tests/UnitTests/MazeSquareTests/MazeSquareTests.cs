using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using MazeRunner.Utils;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace MazeRunner.Tests.UnitTests.MazeSquareTests;

[TestFixture]
public class MazeSquareTests
{
    [OneTimeSetUp]
    public void TestFixtureSetUp()
    {
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
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_AdjacentSquares_ShouldPass()
    {
        // Arrange
        var expectedAdjacentPoints = new List<Point?>
        {
            new Point(x: -1, y: 0), new Point(x: 0, y: -1),
            new Point(x: 1, y: 0), new Point(x: 0, y: 1)
        }.OrderBy(p => p!.Value.X).ThenBy(p => p.Value.Y);

        // Act
        var mazesquare = new MazeSquare(Point.Empty);

        // Assert
        mazesquare.AdjacentSquares.OrderBy(p => p!.Value.X).ThenBy(p => p.Value.Y).Should().BeEquivalentTo(expectedAdjacentPoints);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashCodeVsPlainPointHashCode_ShouldMatch()
    {
        // Arrange
        var point = new Point(687, 67);

        // Act
        var mazesquare = new MazeSquare(point);

        // Assert
        mazesquare.GetHashCode().Should().Be(point.GetHashCode());
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashCodeOnSamePoint_ShouldMatch()
    {
        // Arrange
        var point = new Point(687, 67);

        // Act
        var mazesquare1 = new MazeSquare(point);
        var mazesquare2 = new MazeSquare(point);

        // Assert
        mazesquare1.GetHashCode().Should().Be(mazesquare2.GetHashCode());
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashCodeOnDifferentPoint_ShouldNotMatch()
    {
        // Arrange

        // Act
        var mazesquare1 = new MazeSquare(new Point(100, 100));
        var mazesquare2 = new MazeSquare(new Point(687, 67));

        // Assert
        mazesquare1.GetHashCode().Should().NotBe(mazesquare2.GetHashCode());
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_EqualityOperator_ShouldBeTrue()
    {
        // Arrange
        var point = new Point(384, 3849);

        // Act
        var mazesquare1 = new MazeSquare(point);
        var mazesquare2 = new MazeSquare(point);

        // Assert
        (mazesquare1 == mazesquare2).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_EqualityOperator_ShouldBeFalse()
    {
        // Arrange

        // Act
        var mazesquare1 = new MazeSquare(new Point(3, 3));
        var mazesquare2 = new MazeSquare(new Point(384, 3849));

        // Assert
        (mazesquare1 == mazesquare2).Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_InqualityOperator_ShouldBeFalse()
    {
        // Arrange
        var point = new Point(384, 3849);

        // Act
        var mazesquare1 = new MazeSquare(point);
        var mazesquare2 = new MazeSquare(point);

        // Assert
        (mazesquare1 != mazesquare2).Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_InqualityOperator_ShouldBeTrue()
    {
        // Arrange

        // Act
        var mazesquare1 = new MazeSquare(new Point(100, 100));
        var mazesquare2 = new MazeSquare(new Point(200, 200));

        // Assert
        (mazesquare1 != mazesquare2).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetContainsSoleElement_ShouldBeTrue()
    {
        // Arrange

        // Act
        var squareset = new HashSet<MazeSquare> {new(new Point(100, 100))};

        // Assert
        squareset.Contains(squareset.First()).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetContainsOtherSquareWithSameSpecs_ShouldBeTrue()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(100, 100));

        // Act
        var squareset = new HashSet<MazeSquare> {point1};

        // Assert
        squareset.Contains(point2).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetContainsOtherSquareWithDifferentSpecs_ShouldBeFalse()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(200, 200));

        // Act
        var squareset = new HashSet<MazeSquare> {point1};

        // Assert
        squareset.Contains(point2).Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetTryReplaceExistingSquareWithAnotherSquareOfSameSpecs_PreExistingItemShouldRemainInPlace()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(100, 100));

        // Act
        var squareset = new HashSet<MazeSquare>
        {
            point1,
            point2 //addifnotpresent
        };

        // Assert
        squareset.Should().BeEquivalentTo([point1]);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetTryRemoveExistingItem_ShouldSucceed()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point1Clone = new MazeSquare(new Point(100, 100));

        // Act
        var squareset = new HashSet<MazeSquare> {point1};

        // Assert
        squareset.Remove(point1Clone).Should().Be(true); //order
        squareset.Any().Should().Be(false); //order
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetTryRemoveNonExistingItem_ShouldFail()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(200, 200));

        // Act
        var squareset = new HashSet<MazeSquare> {point1};

        // Assert
        squareset.Remove(point2).Should().Be(false); //order
        squareset.Any().Should().Be(true); //order
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_HashSetAddTwoItemsDifferentSpecs_ShouldSucceed()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(200, 200));

        // Act
        var squareset = new HashSet<MazeSquare> {point1, point2};

        // Assert
        squareset.Should().BeEquivalentTo([point1, point2]);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryContainsSoleElement_ShouldBeTrue()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}};

        // Assert
        squaredict.Contains(squaredict.First()).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryContainsOtherSquareWithSameSpecs_ShouldBeTrue()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(100, 100));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}};

        // Assert
        squaredict.Contains(point2).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryContainsOtherSquareWithDifferentSpecs_ShouldBeFalse()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(200, 200));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}};

        // Assert
        squaredict.Contains(point2).Should().Be(false);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryTryReplaceExistingSquareWithAnotherSquareOfSameSpecs_PreExistingItemShouldGetReplaced()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(100, 100));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}};
        squaredict[point2] = point2; //unlike hashset this replaces point1

        // Assert
        ReferenceEquals(squaredict.Keys.Cast<MazeSquare>().FirstOrDefault(), point2).Should().Be(true);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryAddTwoItemsDifferentSpecs_ShouldSucceed()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(200, 200));
        var point1Clone = new MazeSquare(new Point(100, 100));
        var point2Clone = new MazeSquare(new Point(200, 200));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}, {point2, point2}};

        // Assert
        squaredict.Keys.Cast<MazeSquare>().Should().BeEquivalentTo([point1Clone, point2Clone]);
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryTryRemoveExistingItem_ShouldSucceed()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point1Clone = new MazeSquare(new Point(100, 100));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}};

        // Assert
        squaredict.Remove(point1Clone).Should().Be(true); //order
        squaredict.Any().Should().Be(false); //order
    }

    [Test]
    [Category("Unit.MazeSquare")]
    public void MazeSquare_ReorderableDictionaryTryRemoveNonExistingItem_ShouldFail()
    {
        // Arrange
        var point1 = new MazeSquare(new Point(100, 100));
        var point2 = new MazeSquare(new Point(200, 200));

        // Act
        var squaredict = new ReorderableDictionary<MazeSquare, MazeSquare> {{point1, point1}};

        // Assert
        squaredict.Remove(point2).Should().Be(false); //order
    }
}