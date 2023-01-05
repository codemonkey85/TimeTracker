namespace TimeTracker.Shared.Models;

public class WeekEntryModel
{
    public DateOnly StartDate { get; set; }

    public TimeEntryModel[] TimeEntries { get; set; } = new TimeEntryModel[7];

    public WeekEntryModel(DateOnly startDate)
    {
        StartDate = startDate.StartOfWeek();
        InitializeTimeEntries();
    }

    public WeekEntryModel(DateTime startDate)
    {
        StartDate = DateOnly.FromDateTime(startDate.StartOfWeek());
        InitializeTimeEntries();
    }

    private void InitializeTimeEntries()
    {
        for (var day = 0; day < 7; day++)
        {
            TimeEntries[day] = new TimeEntryModel { Date = StartDate.AddDays(day) };
        }
    }

    public double TotalHours => TimeEntries.Sum(HoursCalculator.CalculateHoursWorked);
}
