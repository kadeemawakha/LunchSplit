
using System;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentIdempotencyTests
{
    [TestMethod]
    public async Task M3_09_DuplicateIdempotencyKey_ReturnsSameTransactionId()
    {
        // Arrange: Create a Square Sandbox processor.
        var processor = new SquarePaymentProcessor();

        // Use the SAME key for both requests.
        string idempotencyKey = Guid.NewGuid().ToString();

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = "cnon:card-nonce-ok",
            IdempotencyKey = idempotencyKey
        };

        // Act: Send the payment twice.
        var firstResult =
            await processor.ProcessPaymentAsync(request);

        var secondResult =
            await processor.ProcessPaymentAsync(request);

        // Assert: Both payments must succeed.
        Assert.IsTrue(firstResult.Success);
        Assert.IsTrue(secondResult.Success);

        // Verify Square returned a transaction ID.
        Assert.IsFalse(
            string.IsNullOrWhiteSpace(firstResult.TransactionId));

        // Verify the same payment was returned.
        Assert.AreEqual(
            firstResult.TransactionId,
            secondResult.TransactionId);
    }
}
