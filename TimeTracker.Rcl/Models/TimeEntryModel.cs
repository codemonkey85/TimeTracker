namespace TimeTracker.Rcl.Models;

public class TimeEntryModel
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
