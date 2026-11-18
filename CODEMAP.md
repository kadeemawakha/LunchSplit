
# LunchSplit Project CodeMap

- **SolutionFile**: `LunchSplit.slnx`
- **CoreSourceRoot**: `LunchSplit`
- **InfraSourceRoot**: `LunchSplit/Payments`
- **TestProjectRoot**: `LunchSplit.Tests`
- **Tier1TestClass**: `LunchSplit.Tests/SquareSandboxTests.cs`
- **Tier2TestClass**: `LunchSplit.Tests/PaymentTimeoutTests.cs`

## Additional Test Files

- `LunchSplit.Tests/PaymentAmountTests.cs`
- `LunchSplit.Tests/PaymentZeroAmountTests.cs`
- `LunchSplit.Tests/PaymentBatchTests.cs`
- `LunchSplit.Tests/PaymentPartialBatchTests.cs`
- `LunchSplit.Tests/PaymentCurrencyTests.cs`
- `LunchSplit.Tests/PaymentIdempotencyTests.cs`
- `LunchSplit.Tests/PaymentStatusTests.cs`

## Payment Implementation

- `LunchSplit/Payments/IPaymentProcessor.cs`
- `LunchSplit/Payments/SquarePaymentProcessor.cs`
- `LunchSplit/Payments/PaymentService.cs`
- `LunchSplit/Payments/PaymentBatchService.cs`
