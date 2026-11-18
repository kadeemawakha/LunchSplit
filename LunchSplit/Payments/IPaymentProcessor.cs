
using System.Threading;
using System.Threading.Tasks;

namespace LunchSplit.Payments;

/// <summary>
/// Defines the contract for processing payments
/// and checking transaction statuses.
/// </summary>
public interface IPaymentProcessor
{
    /// <summary>
    /// Processes a payment asynchronously.
    /// </summary>
    Task<PaymentResult> ProcessPaymentAsync(
        PaymentRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the status of an existing payment.
    /// </summary>
    Task<string> GetTransactionStatusAsync(
        string transactionId);
}
