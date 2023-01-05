namespace TimeTracker.Rcl.Services;

public record DataService(IndexedDbAccessor IndexedDbAccessor) : IDataService
{
    private const string TimeEntriesStore = "TimeEntries";
    private const string WeekEntriesStore = "WeekEntries";

    public async Task<List<TimeEntryModel>> GetTimeEntries() =>
        await IndexedDbAccessor.GetAllValuesAsync<TimeEntryModel>(TimeEntriesStore);

    public async Task<TimeEntryModel> GetTimeEntry(int id) =>
        await IndexedDbAccessor.GetValueAsync<TimeEntryModel>(TimeEntriesStore, id);

    public async Task CreateTimeEntry(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.SetValueAsync(TimeEntriesStore, timeEntry);

    public async Task UpdateTimeEntry(int id, TimeEntryModel timeEntry)
    {
    }

    public async Task RemoveTimeEntry(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.RemoveValueAsync(TimeEntriesStore, timeEntry.Id);
}
