using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchSplit;

namespace LunchSplit.Tests;

[TestClass]
public class ShareTests
{
    [TestMethod]
    public void Share_ShouldStoreAmountCorrectly()
    {
        // Arrange
        decimal expectedAmount = 15.75m;

        // Act
        Share share = new Share(expectedAmount);

        // Assert
        Assert.AreEqual(expectedAmount, share.Amount);
    }
}