
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LunchSplit.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LunchSplit.Tests;

[TestClass]
public class PaymentPartialBatchTests
{
    [TestMethod]
    public async Task M3_07_PartialBatchFailure_TwoSuccessOneFailure()
    {
        // Arrange: Create a mock payment processor.
        var mockProcessor = new Mock<IPaymentProcessor>();

        int callCount = 0;

        mockProcessor
            .Setup(p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                callCount++;

                // Attendee 2 has a declined payment.
                if (callCount == 2)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        Status = "FAILED",
                        ErrorCode = "CARD_DECLINED"
                    };
                }

                return new PaymentResult
                {
                    Success = true,
                    Status = "COMPLETED",
                    TransactionId = Guid.NewGuid().ToString()
                };
            });

        var service = new PaymentService(mockProcessor.Object);
        var batchService = new PaymentBatchService(service);

        var requests = new List<PaymentRequest>();

        // Create payments for three attendees.
        for (int i = 0; i < 3; i++)
        {
            requests.Add(new PaymentRequest
            {
                Amount = 2500,
                Currency = "CAD",
                SourceId = "test-source",
                IdempotencyKey = Guid.NewGuid().ToString()
            });
        }

        // Act: Process the batch.
        var summary = await batchService.ProcessBatchAsync(requests);

        // Assert: Two succeeded and one failed.
        Assert.AreEqual(2, summary.SuccessCount);
        Assert.AreEqual(1, summary.FailureCount);

        // Verify all three payments were attempted.
        mockProcessor.Verify(
            p => p.ProcessPaymentAsync(
                It.IsAny<PaymentRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(3));
    }
}
