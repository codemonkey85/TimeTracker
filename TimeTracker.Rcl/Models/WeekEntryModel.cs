namespace TimeTracker.Rcl.Models;

public class WeekEntryModel
{
    private DateOnly? startDate;

    public DateOnly? StartDate
    {
        get => startDate;
        set
        {
            startDate = value;
            Reload();
        }
    }

    public TimeEntryModel[] TimeEntries { get; set; } = new TimeEntryModel[7];

    public WeekEntryModel(DateTime startDate)
    {
        StartDate = DateOnly.FromDateTime(startDate.StartOfWeek());
        Reload();
    }

    private void Reload()
    {
        if (StartDate is null)
        {
            return;
        }
        for (var day = 0; day < 7; day++)
        {
            TimeEntries[day] = new TimeEntryModel { Date = StartDate.Value.AddDays(day) };
        }
    }

    public double TotalHours => TimeEntries.Sum(HoursCalculator.CalculateHoursWorked);
}
