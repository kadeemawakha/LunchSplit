
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchSplit.Payments;

namespace LunchSplit.Tests;

[TestClass]
public class SquarePaymentProcessorTests
{
    [TestMethod]
    public void PaymentRequest_StoresCorrectAmountAndCurrency()
    {
        // Arrange: Create a payment request for $25.00 CAD.
        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = "test-source"
        };

        // Assert: Verify the payment information.
        Assert.AreEqual(2500L, request.Amount);
        Assert.AreEqual("CAD", request.Currency);
        Assert.AreEqual("test-source", request.SourceId);
    }


    [TestMethod]
    public async Task NegativePaymentAmount_ThrowsException()
    {
        // Arrange: Create a payment processor and invalid request.
        var processor = new SquarePaymentProcessor();

        var request = new PaymentRequest
        {
            Amount = -100,
            Currency = "CAD",
            SourceId = "test-source"
        };

        // Act and Assert: A negative amount must be rejected.

        await Assert.ThrowsAsync<ArgumentException>(
            async () => await processor.ProcessPaymentAsync(request));
    }

    [TestMethod]
    public async Task InvalidCurrency_ThrowsException()
    {
        // Arrange: Create a processor and a payment in USD.
        var processor = new SquarePaymentProcessor();

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "USD",
            SourceId = "test-source"
        };

        // Act and Assert: Only CAD payments are accepted.
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await processor.ProcessPaymentAsync(request));
    }


    [TestMethod]
    public async Task MissingPaymentSource_ThrowsException()
    {
        // Arrange: Create a payment without a source ID.
        var processor = new SquarePaymentProcessor();

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = ""
        };

        // Act and Assert: A missing payment source is rejected.
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await processor.ProcessPaymentAsync(request));
    }


    [TestMethod]
    public async Task NullPaymentRequest_ThrowsException()
    {
        // Arrange: Create a payment processor.
        var processor = new SquarePaymentProcessor();

        // Act and Assert: A null request must be rejected.
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await processor.ProcessPaymentAsync(null!));
    }


}
