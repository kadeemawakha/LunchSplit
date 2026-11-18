
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentZeroAmountTests
{
    [TestMethod]
    public async Task M3_05_ZeroAmount_NeverCallsProcessor()
    {
        // Arrange: Create a mock payment processor.
        var mockProcessor = new Mock<IPaymentProcessor>();
        var service = new PaymentService(mockProcessor.Object);

        var request = new PaymentRequest
        {
            Amount = 0,
            Currency = "CAD",
            SourceId = "test-source",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        // Act and Assert: A zero amount must be rejected.
        await Assert.ThrowsAsync<ArgumentException>(
     async () => await service.ProcessPaymentAsync(request));

        // Verify the payment processor was never called.
        mockProcessor.Verify(
            p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
