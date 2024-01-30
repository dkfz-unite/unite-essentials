namespace Unite.Essentials.Extensions;

public static class DateOnlyExtensions
{
    public static int? RelativeFrom(this DateOnly? eventDate, DateOnly? anchorDate)
    {
        return anchorDate != null && eventDate != null
            ? eventDate.Value.RelativeFrom(anchorDate.Value)
            : null;
    }

    public static int? RelativeFrom(this DateOnly? eventDate, DateOnly anchorDate)
    {
        return eventDate != null
            ? eventDate.Value.RelativeFrom(anchorDate)
            : null;
    }

    public static int? RelativeFrom(this DateOnly eventDate, DateOnly? anchorDate)
    {
        return anchorDate != null
            ? eventDate.RelativeFrom(anchorDate.Value)
            : null;
    }

    public static int RelativeFrom(this DateOnly eventDate, DateOnly anchorDate)
    {
        return eventDate.DayNumber - anchorDate.DayNumber + 1;
    }
}
