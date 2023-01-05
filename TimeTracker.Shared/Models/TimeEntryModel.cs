namespace TimeTracker.Shared.Models;

public class TimeEntryModel
{
    public int Id { get; set; }

    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
