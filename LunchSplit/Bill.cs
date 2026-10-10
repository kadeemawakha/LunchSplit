namespace LunchSplit;

public class Bill
{
    public decimal Subtotal { get; }

    public Bill(decimal subtotal)
    {
        if (subtotal < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(subtotal),
                "Subtotal cannot be negative.");
        }

        Subtotal = subtotal;
    }
}