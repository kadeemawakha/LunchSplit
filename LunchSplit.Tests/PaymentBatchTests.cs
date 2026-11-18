
using System;
using System.Threading;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentBatchTests
{
    [TestMethod]
    public async Task M3_06_BatchPayment_ProcessesFourAttendees()
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

        // Create payments for four attendees.
        var amounts = new long[] { 2500, 2500, 2500, 2500 };

        // Act: Process all four payments.
        foreach (var amount in amounts)
        {
            var request = new PaymentRequest
            {
                Amount = amount,
                Currency = "CAD",
                SourceId = "test-source",
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            var result = await service.ProcessPaymentAsync(request);

            Assert.IsTrue(result.Success);
        }

        // Assert: Verify exactly four payment calls.
        mockProcessor.Verify(
            p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(4));
    }
}
