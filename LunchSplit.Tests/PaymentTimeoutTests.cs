
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentTimeoutTests
{
    [TestMethod]
    public async Task M3_03_NetworkTimeout_ThrowsPaymentGatewayTimeoutException()
    {
        // Arrange: Create a fake payment processor.
        var mockProcessor = new Mock<IPaymentProcessor>();

        // Simulate a network failure.
        mockProcessor
            .Setup(p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException(
                "Simulated network timeout."));

        var service = new PaymentService(mockProcessor.Object);

        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = "test-source",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        // Act and Assert: Verify the timeout exception.

        await Assert.ThrowsAsync<PaymentGatewayTimeoutException>(
            async () => await service.ProcessPaymentAsync(request));


        // Verify the payment processor was called once.
        mockProcessor.Verify(
            p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
