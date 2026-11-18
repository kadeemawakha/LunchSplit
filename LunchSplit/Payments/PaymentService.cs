
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LunchSplit.Payments;

public class PaymentService
{
    private readonly IPaymentProcessor _processor;

    public PaymentService(IPaymentProcessor processor)
    {
        _processor = processor;
    }

    public async Task<PaymentResult> ProcessPaymentAsync(
        PaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _processor.ProcessPaymentAsync(
                request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            throw new PaymentGatewayTimeoutException(
                "The payment gateway timed out.", ex);
        }
    }
}
