namespace TimeTracker.Rcl.Services;

public interface IDataService
{
    Task<List<TimeEntryModel>> TestAsync();
}
