namespace TimeTracker.Shared.Models;

public class TimeEntryModel
{
    public DateTime? Date { get; init; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }
}
