namespace TimeTracker.Shared.Models;

public class TimeEntryModel
{
    public DateTime? Date { get; init; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }
}
