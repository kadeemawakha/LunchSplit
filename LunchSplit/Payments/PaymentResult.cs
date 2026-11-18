
namespace LunchSplit.Payments;

/// <summary>
/// Represents the outcome of a payment attempt.
/// </summary>
public class PaymentResult
{
    /// <summary>
    /// Indicates whether the payment succeeded.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Unique transaction identifier returned by Square.
    /// </summary>
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Current payment status, such as COMPLETED or FAILED.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Error message when a payment fails.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}
