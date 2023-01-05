namespace TimeTracker.Rcl.Services;

public record DataService(IndexedDbAccessor IndexedDbAccessor) : IDataService
{
    private const string TimeEntriesStore = "TimeEntries";
    private const string WeekEntriesStore = "WeekEntries";

    public async Task<List<TimeEntryModel>> GetTimeEntries()
    {
        await IndexedDbAccessor.WaitForReference();
        var result = await IndexedDbAccessor.accessorJsRef.Value.InvokeAsync<List<TimeEntryModel>>("getAll", TimeEntriesStore);

        return result;
    }

    public async Task<TimeEntryModel> GetTimeEntry(int id)
    {
        await IndexedDbAccessor.WaitForReference();
        var result = await IndexedDbAccessor.accessorJsRef.Value.InvokeAsync<TimeEntryModel>("get", TimeEntriesStore, id);

        return result;
    }

    public async Task CreateTimeEntry(TimeEntryModel timeEntry)
    {
    }

    public async Task UpdateTimeEntry(int id, TimeEntryModel timeEntry)
    {
    }

    public async Task RemoveTimeEntry(TimeEntryModel timeEntry)
    {
    }
}
