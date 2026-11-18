
namespace LunchSplit;

public class Splitter
{
    public decimal[] CalculateShares(decimal total, int people)
    {
        if (total < 0)
        {
            throw new ArgumentException("Total cannot be negative");
        }

        if (people <= 0)
        {
            throw new ArgumentException("Number of people must be greater than zero");
        }

        decimal share = total / people;
        decimal[] shares = new decimal[people];

        for (int i = 0; i < people; i++)
        {
            shares[i] = share;
        }

        return shares;
    }
}
