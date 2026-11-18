
using System;

namespace LunchSplit.Payments;

/// <summary>
/// Stores the information needed to process a payment.
/// </summary>
public class PaymentRequest
{
    /// <summary>
    /// Payment amount in cents.
    /// Example: 2500 means $25.00 CAD.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Three-letter currency code.
    /// </summary>
    public string Currency { get; set; } = "CAD";

    /// <summary>
    /// Square Sandbox payment source token.
    /// </summary>
    public string SourceId { get; set; } = string.Empty;

    /// <summary>
    /// Unique key that helps prevent duplicate payments.
    /// </summary>
    public string IdempotencyKey { get; set; }
        = Guid.NewGuid().ToString();
}
