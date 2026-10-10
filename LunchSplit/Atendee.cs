namespace LunchSplit;

public class Attendee
{
    public string Name { get; }

    public Attendee(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Attendee name cannot be empty.",
                nameof(name));
        }

        Name = name;
    }
}