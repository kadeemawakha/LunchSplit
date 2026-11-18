
using System;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchSplit.Tests;

/// <summary>
/// Tests retrieving payment status from Square Sandbox.
/// </summary>
[TestClass]
public class PaymentStatusTests
{
    [TestMethod]
    public async Task M3_10_TransactionStatus_ReturnsCompleted()
    {
        // Arrange: Create the Square Sandbox processor.
        var processor = new SquarePaymentProcessor();

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = "cnon:card-nonce-ok",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        // Act: Create a successful payment.
        var paymentResult =
            await processor.ProcessPaymentAsync(request);

        // Verify the payment was successful.
        Assert.IsTrue(paymentResult.Success);
        Assert.IsFalse(
            string.IsNullOrWhiteSpace(
                paymentResult.TransactionId));

        // Act: Retrieve the payment status from Square.
        var status =
            await processor.GetTransactionStatusAsync(
                paymentResult.TransactionId);

        // Assert: The payment must be completed.
        Assert.AreEqual("COMPLETED", status);
    }
}
