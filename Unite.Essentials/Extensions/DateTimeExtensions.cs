namespace Unite.Essentials.Extensions;

public static class DateTimeExtensions
{
    public static int? RelativeFrom(this DateTime? eventDate, DateTime? anchorDate)
    {
        return anchorDate != null && eventDate != null
            ? eventDate.Value.RelativeFrom(anchorDate.Value)
            : null;
    }

    public static int? RelativeFrom(this DateTime? eventDate, DateTime anchorDate)
    {
        return eventDate != null
            ? eventDate.Value.RelativeFrom(anchorDate)
            : null;
    }

    public static int? RelativeFrom(this DateTime eventDate, DateTime? anchorDate)
    {
        return anchorDate != null
            ? eventDate.RelativeFrom(anchorDate.Value)
            : null;
    }

    public static int RelativeFrom(this DateTime eventDate, DateTime anchorDate)
    {
        return (Normalise(eventDate) - Normalise(anchorDate)).Days + 1;
    }


    private static DateTime Normalise(DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 12, 00, 00);
    }
}
