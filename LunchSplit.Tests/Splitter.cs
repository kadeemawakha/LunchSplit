
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchSplit;
using System;

namespace LunchSplit.Tests;

[TestClass]
public class SplitterTests
{
    [TestMethod]
    public void NegativeTotal_ThrowsException()
    {
        // Arrange
        var splitter = new Splitter();

        // Act and Assert
        Assert.Throws<ArgumentException>(
            () => splitter.CalculateShares(-10m, 2));
    }

    [TestMethod]
    public void ZeroPeople_ThrowsException()
    {
        // Arrange
        var splitter = new Splitter();

        // Act and Assert
        Assert.Throws<ArgumentException>(
            () => splitter.CalculateShares(100m, 0));
    }

    [TestMethod]
    public void ValidBill_ReturnsEqualShares()
    {
        // Arrange
        var splitter = new Splitter();

        // Act
        decimal[] result =
            splitter.CalculateShares(100m, 4);

        // Assert
        Assert.AreEqual(4, result.Length);

        foreach (decimal share in result)
        {
            Assert.AreEqual(25m, share);
        }
    }

    [TestMethod]
    public void OnePerson_ReturnsFullAmount()
    {
        // Arrange
        var splitter = new Splitter();

        // Act
        decimal[] result = splitter.CalculateShares(100m, 1);

        // Assert
        Assert.HasCount(1, result);
        Assert.AreEqual(100m, result[0]);
    }

}

