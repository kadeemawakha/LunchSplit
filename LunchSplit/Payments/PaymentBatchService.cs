
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LunchSplit.Payments;

public class PaymentBatchService
{
    private readonly PaymentService _paymentService;

    public PaymentBatchService(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<PaymentBatchSummary> ProcessBatchAsync(
        IEnumerable<PaymentRequest> requests)
    {
        var summary = new PaymentBatchSummary();

        foreach (var request in requests)
        {
            var result = await _paymentService.ProcessPaymentAsync(request);

            if (result.Success)
                summary.SuccessCount++;
            else
                summary.FailureCount++;
        }

        return summary;
    }
}
