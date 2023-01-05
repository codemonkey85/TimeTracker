namespace TimeTracker.Rcl.Services;

public record DataService(IndexedDbAccessor IndexedDbAccessor) : IDataService
{
    private const string TimeEntriesStore = "TimeEntries";
    private const string WeekEntriesStore = "WeekEntries";

    public async Task<List<TimeEntryModel>?> GetAllTimeEntries() =>
        await IndexedDbAccessor.GetAllValuesAsync<TimeEntryModel>(TimeEntriesStore);

    public async Task<TimeEntryModel?> GetTimeEntry(int id) =>
        await IndexedDbAccessor.GetValueAsync<TimeEntryModel>(TimeEntriesStore, id);

    public async Task CreateTimeEntry(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.CreateValueAsync(TimeEntriesStore, timeEntry);

    public async Task UpdateTimeEntry(int id, TimeEntryModel timeEntry)
    {
        var foundTimeEntry = await GetTimeEntry(id);
        if (foundTimeEntry is null)
        {
            return;
        }

        foundTimeEntry.StartTime = timeEntry.StartTime;
        foundTimeEntry.EndTime = timeEntry.EndTime;

        await IndexedDbAccessor.UpdateValueAsync(TimeEntriesStore, foundTimeEntry);
    }

    public async Task DeleteTimeEntry(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.DeleteValueAsync(TimeEntriesStore, timeEntry.Id);

    public async Task<List<WeekEntryModel>?> GetAllWeekEntries() =>
        await IndexedDbAccessor.GetAllValuesAsync<WeekEntryModel>(WeekEntriesStore);

    public async Task<WeekEntryModel?> GetWeekEntry(int id) =>
        await IndexedDbAccessor.GetValueAsync<WeekEntryModel>(WeekEntriesStore, id);

    public async Task CreateWeekEntry(WeekEntryModel weekEntry) =>
        await IndexedDbAccessor.CreateValueAsync(WeekEntriesStore, weekEntry);

    public async Task UpdateWeekEntry(int id, WeekEntryModel weekEntry)
    {
        var foundWeekEntry = await GetWeekEntry(id);
        if (foundWeekEntry is null)
        {
            return;
        }

        foundWeekEntry.TimeEntries = weekEntry.TimeEntries;

        await IndexedDbAccessor.UpdateValueAsync(WeekEntriesStore, foundWeekEntry);
    }

    public async Task DeleteWeekEntry(WeekEntryModel weekEntry) =>
        await IndexedDbAccessor.DeleteValueAsync(WeekEntriesStore, weekEntry.Id);
}
