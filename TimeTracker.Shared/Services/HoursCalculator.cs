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

    private static TimeSpan ConvertTime(TimeSpan time) => time switch
    {
        { Minutes: < 8 } => new TimeSpan(hours: time.Hours, minutes: 0, 0),
        { Minutes: < 23 } => new TimeSpan(hours: time.Hours, minutes: 15, 0),
        { Minutes: < 38 } => new TimeSpan(hours: time.Hours, minutes: 30, 0),
        { Minutes: < 53 } => new TimeSpan(hours: time.Hours, minutes: 45, 0),
        { Minutes: >= 53 } => new TimeSpan(hours: time.Hours + 1, minutes: 0, 0),
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
