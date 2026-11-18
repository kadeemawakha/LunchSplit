
using System;

namespace LunchSplit.Payments;

/// <summary>
/// Exception thrown when a payment gateway
/// experiences a network timeout.
/// </summary>
public class PaymentGatewayTimeoutException : Exception
{
    public PaymentGatewayTimeoutException()
        : base("The payment gateway timed out.")
    {
    }

    public PaymentGatewayTimeoutException(string message)
        : base(message)
    {
    }

    public PaymentGatewayTimeoutException(
        string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
