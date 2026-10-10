using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchSplit;

namespace LunchSplit.Tests;

[TestClass]
public class AttendeeTests
{
    [TestMethod]
    public void Attendee_ShouldStoreNameCorrectly()
    {
        // Arrange
        string expectedName = "Alex";

        // Act
        Attendee attendee = new Attendee(expectedName);

        // Assert
        Assert.AreEqual(expectedName, attendee.Name);
    }

    [TestMethod]
    public void Attendee_ShouldRejectEmptyName()
    {
        // Arrange
        string invalidName = "";

        // Act and Assert
        Assert.ThrowsExactly<ArgumentException>(
            () => new Attendee(invalidName));
    }
}