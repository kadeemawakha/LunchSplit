
using System;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentCurrencyTests
{
    [TestMethod]
    public async Task M3_08_UnsupportedCurrency_ReturnsValidationError()
    {
        // Arrange
        var processor = new SquarePaymentProcessor();

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "EUR",
            SourceId = "cnon:card-nonce-ok",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        // Act
        var result = await processor.ProcessPaymentAsync(request);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("FAILED", result.Status);
        Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorCode));
        Assert.AreEqual(400, result.HttpStatusCode);
    }
}
