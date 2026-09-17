using GameOfLife.Core;

namespace GameOfLife.Test.Units;

public class RulesTest
{
    [Test]
    public void IsAliveNextFrame_ReturnsFalse_WhenAliveCellHasNoNeighbours()
    {
        // Arrange
        const bool isAlive = true;
        const int neighbours = 0;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsFalse_WhenAliveCellHasOneNeighbour()
    {
        // Arrange
        const bool isAlive = true;
        const int neighbours = 1;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsTrue_WhenAliveCellHasTwoNeighbours()
    {
        // Arrange
        const bool isAlive = true;
        const int neighbours = 2;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsTrue_WhenAliveCellHasThreeNeighbours()
    {
        // Arrange
        const bool isAlive = true;
        const int neighbours = 3;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsFalse_WhenAliveCellHasFourNeighbours()
    {
        // Arrange
        const bool isAlive = true;
        const int neighbours = 4;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsTrue_WhenDeadCellHasExactlyThreeNeighbours()
    {
        // Arrange
        const bool isAlive = false;
        const int neighbours = 3;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsFalse_WhenDeadCellHasMoreThanThreeNeighbours()
    {
        // Arrange
        const bool isAlive = false;
        const int neighbours = 4;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAliveNextFrame_ReturnsFalse_WhenDeadCellHasFewerThanThreeNeighbours()
    {
        // Arrange
        const bool isAlive = false;
        const int neighbours = 2;

        // Act
        var result = Rules.IsAliveNextFrame(isAlive, neighbours);

        // Assert
        Assert.That(result, Is.False);
    }
}
