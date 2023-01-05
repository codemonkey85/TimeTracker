namespace TimeTracker.Rcl.Services;

public record DataService(IndexedDbAccessor IndexedDbAccessor) : IDataService
{
    private const string TimeEntriesStore = "TimeEntries";
    private const string WeekEntriesStore = "WeekEntries";

    public async Task<List<TimeEntryModel>?> GetTimeEntries() =>
        await IndexedDbAccessor.GetAllValuesAsync<TimeEntryModel>(TimeEntriesStore);

    public async Task<TimeEntryModel?> GetTimeEntry(int id) =>
        await IndexedDbAccessor.GetValueAsync<TimeEntryModel>(TimeEntriesStore, id);

    public async Task CreateTimeEntry(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.AddValueAsync(TimeEntriesStore, timeEntry);

    public async Task UpdateTimeEntry(int id, TimeEntryModel timeEntry)
    {
        var foundTimeEntry = await GetTimeEntry(id);
        if (foundTimeEntry is null)
        {
            return;
        }

        foundTimeEntry.StartTime = timeEntry.StartTime;
        foundTimeEntry.EndTime = timeEntry.EndTime;
        await IndexedDbAccessor.PutValueAsync(TimeEntriesStore, foundTimeEntry);
    }

    public async Task DeleteTimeEntry(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.DeleteValueAsync(TimeEntriesStore, timeEntry.Id);
}
