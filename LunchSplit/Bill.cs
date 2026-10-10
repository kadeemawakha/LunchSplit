namespace LunchSplit;

public class Bill
{
    public decimal Subtotal { get; }

    public Bill(decimal subtotal)
    {
        Subtotal = subtotal;
    }
}