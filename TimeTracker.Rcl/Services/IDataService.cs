namespace TimeTracker.Rcl.Services;

public interface IDataService
{
    Task<List<TimeEntryModel>?> GetTimeEntries();

    Task<TimeEntryModel?> GetTimeEntry(int id);

    Task CreateTimeEntry(TimeEntryModel timeEntry);

    Task UpdateTimeEntry(int id, TimeEntryModel timeEntry);

    Task DeleteTimeEntry(TimeEntryModel timeEntry);
}
