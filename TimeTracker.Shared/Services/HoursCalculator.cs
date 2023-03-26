namespace TimeTracker.Shared.Services;

public static class HoursCalculator
{
    public static double CalculateHoursWorked(TimeEntryModel timeEntryModel)
    {
        if (timeEntryModel is { StartTime: null } or { EndTime: null })
        {
            return 0D;
        }
        var (endTime, startTime) = (ConvertTime(timeEntryModel.EndTime.Value), ConvertTime(timeEntryModel.StartTime.Value));
        return (endTime - startTime).TotalHours;
    }

    private static DateTime ConvertTime(DateTime time) => time switch
    {
        { Minute: < 8 } => new DateTime(year: 1, month: 1, day: 1, hour: time.Hour, minute: 0, 0),
        { Minute: < 23 } => new DateTime(year: 1, month: 1, day: 1, hour: time.Hour, minute: 15, 0),
        { Minute: < 38 } => new DateTime(year: 1, month: 1, day: 1, hour: time.Hour, minute: 30, 0),
        { Minute: < 53 } => new DateTime(year: 1, month: 1, day: 1, hour: time.Hour, minute: 45, 0),
        { Minute: >= 53 } => new DateTime(year: 1, month: 1, day: 1, hour: time.Hour + 1, minute: 0, 0),
    };

    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = Constants.StartOfWeek)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public static DateOnly StartOfWeek(this DateOnly dt, DayOfWeek startOfWeek = Constants.StartOfWeek)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff);
    }
}
