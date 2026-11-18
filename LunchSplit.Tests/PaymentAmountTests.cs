
using System;
using System.Threading;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentAmountTests
{
    [TestMethod]
    public async Task M3_04_PaymentAmount_VerifiesExactAmount()
    {
        // Arrange: Create a mock payment processor.
        var mockProcessor = new Mock<IPaymentProcessor>();

        mockProcessor
            .Setup(p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentResult
            {
                Success = true,
                Status = "COMPLETED",
                TransactionId = "test-transaction"
            });

        var service = new PaymentService(mockProcessor.Object);

        // $25.00 CAD = 2500 cents.
        var request = new PaymentRequest
        {
            Amount = 2500,
            Currency = "CAD",
            SourceId = "test-source",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        // Act: Process the payment.
        await service.ProcessPaymentAsync(request);

        // Assert: Verify the exact amount was passed once.
        mockProcessor.Verify(
            p => p.ProcessPaymentAsync(
                It.Is<PaymentRequest>(r => r.Amount == 2500),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
