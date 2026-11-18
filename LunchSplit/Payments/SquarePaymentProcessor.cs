
using System;
using System.Threading;
using System.Threading.Tasks;
using Square;
using Square.Payments;

namespace LunchSplit.Payments;

/// <summary>
/// Processes payments through the Square Sandbox API.
/// </summary>
public class SquarePaymentProcessor : IPaymentProcessor
{
    private readonly SquareClient _client;
    private readonly string _locationId;

    /// <summary>
    /// Creates a payment processor using Sandbox credentials.
    /// </summary>
    public SquarePaymentProcessor()
    {
        string token = Environment.GetEnvironmentVariable(
            "SQUARE_ACCESS_TOKEN")
            ?? throw new InvalidOperationException(
                "Square access token is missing.");

        _locationId = Environment.GetEnvironmentVariable(
            "SQUARE_LOCATION_ID")
            ?? throw new InvalidOperationException(
                "Square location ID is missing.");

        // Connect only to Square's test environment.

        _client = new SquareClient(
            token,
            new ClientOptions
            {
                BaseUrl = SquareEnvironment.Sandbox,
                Timeout = TimeSpan.FromSeconds(30)
            });

    }

    /// <summary>
    /// Sends a payment request to Square Sandbox.
    /// </summary>
    public async Task<PaymentResult> ProcessPaymentAsync(
        PaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);


        // Validate the payment before contacting Square.
        if (request.Amount <= 0)
            throw new ArgumentException(
                "Payment amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(request.SourceId))
            throw new ArgumentException(
                "Payment source is required.");

        // Create the payment request for Square.
        var squareRequest = new CreatePaymentRequest
        {
            SourceId = request.SourceId,
            IdempotencyKey = request.IdempotencyKey,
            AmountMoney = new Money
            {
                Amount = request.Amount,
                Currency = request.Currency.ToUpperInvariant() switch
                {
                    "CAD" => Currency.Cad,
                    "EUR" => Currency.Eur,
                    _ => throw new ArgumentException(
                        "Unsupported currency.")
                }
            },
            LocationId = _locationId,
            Autocomplete = true
        };

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Send the payment to Square Sandbox.
            var response = await _client.Payments.CreateAsync(
                squareRequest);

            var payment = response.Payment;

            return new PaymentResult
            {
                Success = payment?.Status?.ToString()
                    == "COMPLETED",
                TransactionId = payment?.Id ?? string.Empty,
                Status = payment?.Status?.ToString()
                    ?? "UNKNOWN"
            };
        }

        catch (SquareApiException ex)
        {
            // Return the Square error details.
            return new PaymentResult
            {
                Success = false,
                Status = "FAILED",
                ErrorMessage = ex.Message,
                ErrorCode = ex.Errors?.FirstOrDefault()?.Code.ToString()
    ?? string.Empty,
                HttpStatusCode = (int)ex.StatusCode
            };
        }

    }


    /// <summary>
    /// Retrieves the status of an existing Square payment.
    /// </summary>
    public async Task<string> GetTransactionStatusAsync(
        string transactionId)
    {
        if (string.IsNullOrWhiteSpace(transactionId))
            throw new ArgumentException(
                "Transaction ID is required.");

        // Retrieve the payment from Square Sandbox.
        var response = await _client.Payments.GetAsync(
            new GetPaymentsRequest
            {
                PaymentId = transactionId
            });

        // Return the current transaction status.
        return response.Payment?.Status?.ToString()
            ?? "UNKNOWN";
    }

}

