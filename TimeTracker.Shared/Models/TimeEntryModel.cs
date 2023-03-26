namespace TimeTracker.Shared.Models;

public class TimeEntryModel
{
    public DateTime? Date { get; init; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}
