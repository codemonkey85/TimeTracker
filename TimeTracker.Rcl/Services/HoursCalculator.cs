namespace TimeTracker.Rcl.Services;

public static class HoursCalculator
{
    public static double CalculateHoursWorked(TimeEntryModel timeEntryModel)
    {
        var (endTime, startTime) = (ConvertTime(timeEntryModel.EndTime), ConvertTime(timeEntryModel.StartTime));
        return (endTime - startTime).TotalHours;
    }

    private static TimeOnly ConvertTime(TimeOnly time) => time switch
    {
        { Minute: < 8 } => new TimeOnly(time.Hour, 0),
        { Minute: < 23 } => new TimeOnly(time.Hour, 15),
        { Minute: < 38 } => new TimeOnly(time.Hour, 30),
        { Minute: < 53 } => new TimeOnly(time.Hour, 45),
        { Minute: >= 53 } => new TimeOnly(time.Hour + 1, 0),
    };

    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}
