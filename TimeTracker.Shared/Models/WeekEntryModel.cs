namespace TimeTracker.Shared.Models;

public class WeekEntryModel
{
    public int Id { get; set; }

    public DateTime? StartDate { get; set; }

    public List<TimeEntryModel> TimeEntries { get; set; } = new();

    [JsonIgnore]
    public bool IsNew { get; set; }

    public WeekEntryModel(DateTime? startDate)
    {
        StartDate = startDate?.StartOfWeek();
        InitializeTimeEntries();
    }

    private void InitializeTimeEntries()
    {
        for (var day = 0; day < 7; day++)
        {
            TimeEntries.Add(new TimeEntryModel { Date = StartDate?.AddDays(day) });
        }
    }

    [JsonIgnore]
    public double TotalHours => TimeEntries.Sum(HoursCalculator.CalculateHoursWorked);
}
