
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchSplit;

namespace LunchSplit.Tests;

[TestClass]
public class BillTests
{
    [TestMethod]
    public void Bill_ShouldStoreSubtotalCorrectly()
    {
        // Arrange: Set up the subtotal
        decimal expectedSubtotal = 25.50m;

        // Act: Create a bill
        Bill bill = new Bill(expectedSubtotal);

        // Assert: Check the subtotal
        Assert.AreEqual(expectedSubtotal, bill.Subtotal);
    }

    [TestMethod]
    public void Bill_ShouldAllowZeroSubtotal()
    {
        // Arrange
        decimal expectedSubtotal = 0.00m;

        // Act
        Bill bill = new Bill(expectedSubtotal);

        // Assert
        Assert.AreEqual(expectedSubtotal, bill.Subtotal);
    }

    [TestMethod]
    public void Bill_ShouldRejectNegativeSubtotal()
    {
        // Arrange
        decimal invalidSubtotal = -10.00m;

        // Act and Assert
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => new Bill(invalidSubtotal));
    }
}


