
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchSplit.Payments;

namespace LunchSplit.Tests;

[TestClass]
public class SquareSandboxTests
{
    // M3-01: Verify a successful Square Sandbox payment.
    [TestMethod]
    public async Task M3_01_SuccessfulPayment_ReturnsTransactionId()
    {
        // Arrange: Create a $25.00 CAD test payment.
        var processor = new SquarePaymentProcessor();

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = "cnon:card-nonce-ok",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        // Act: Send the payment to Square Sandbox.
        var result = await processor.ProcessPaymentAsync(request);

        // Assert: Verify successful payment and transaction ID.
        Assert.IsTrue(
    result.Success,
    $"Payment failed. Status: {result.Status}. Error: {result.ErrorMessage}"
);
        Assert.IsFalse(string.IsNullOrWhiteSpace(
            result.TransactionId));
    }
}
