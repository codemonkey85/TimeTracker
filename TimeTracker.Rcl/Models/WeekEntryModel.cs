namespace TimeTracker.Rcl.Models;

public class WeekEntryModel
{
    public DateOnly? StartDate { get; set; }

    public TimeEntryModel[] TimeEntries { get; set; } = new TimeEntryModel[7];

    public WeekEntryModel(DateTime startDate)
    {
        StartDate = DateOnly.FromDateTime(startDate.StartOfWeek());
        for (var day = 0; day < 7; day++)
        {
            TimeEntries[day] = new TimeEntryModel { Date = StartDate.Value.AddDays(day) };
        }
    }

    public double TotalHours => TimeEntries.Sum(HoursCalculator.CalculateHoursWorked);
}
