
using System.Threading;
using System.Threading.Tasks;

namespace LunchSplit.Payments;


/// Defines the contract for processing payments.
/// Implementations can use Square or a mock processor.
public interface IPaymentProcessor
{
    /// <summary>
    /// Processes a payment request asynchronously.
    /// </summary>
    /// <param name="request">
    /// The payment information to process.
    /// </param>
    /// <param name="cancellationToken">
    /// Allows the operation to be cancelled.
    /// </param>
    /// <returns>The result of the payment attempt.</returns>
    /// <summary>
    Task<PaymentResult> ProcessPaymentAsync(
        PaymentRequest request,
        CancellationToken cancellationToken = default);
}
