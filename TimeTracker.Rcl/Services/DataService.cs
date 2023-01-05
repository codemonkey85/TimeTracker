namespace TimeTracker.Rcl.Services;

public record DataService(IndexedDbAccessor IndexedDbAccessor) : IDataService
{
    private const string TimeEntriesStore = "TimeEntries";
    private const string WeekEntriesStore = "WeekEntries";

    public async Task<List<TimeEntryModel>?> GetAllTimeEntriesAsync() =>
        await IndexedDbAccessor.GetAllValuesAsync<TimeEntryModel>(TimeEntriesStore);

    public async Task<TimeEntryModel?> GetTimeEntryAsync(int id) =>
        await IndexedDbAccessor.GetValueAsync<TimeEntryModel>(TimeEntriesStore, id);

    public async Task CreateTimeEntryAsync(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.CreateValueAsync(TimeEntriesStore, timeEntry);

    public async Task UpdateTimeEntryAsync(int id, TimeEntryModel timeEntry)
    {
        var foundTimeEntry = await GetTimeEntryAsync(id);
        if (foundTimeEntry is null)
        {
            return;
        }

        foundTimeEntry.StartTime = timeEntry.StartTime;
        foundTimeEntry.EndTime = timeEntry.EndTime;

        await IndexedDbAccessor.UpdateValueAsync(TimeEntriesStore, foundTimeEntry);
    }

    public async Task DeleteTimeEntryAsync(TimeEntryModel timeEntry) =>
        await IndexedDbAccessor.DeleteValueAsync(TimeEntriesStore, timeEntry.Id);

    public async Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateOnly indexValue) =>
        await IndexedDbAccessor.GetValueByIndexAsync<WeekEntryModel>(WeekEntriesStore, "startDate", indexValue);

    public async Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync() =>
        await IndexedDbAccessor.GetAllValuesAsync<WeekEntryModel>(WeekEntriesStore);

    public async Task<WeekEntryModel?> GetWeekEntryAsync(int id) =>
        await IndexedDbAccessor.GetValueAsync<WeekEntryModel>(WeekEntriesStore, id);

    public async Task CreateWeekEntryAsync(WeekEntryModel weekEntry) =>
        await IndexedDbAccessor.CreateValueAsync(WeekEntriesStore, weekEntry);

    public async Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry)
    {
        var foundWeekEntry = await GetWeekEntryAsync(id);
        if (foundWeekEntry is null)
        {
            return;
        }

        foundWeekEntry.TimeEntries = weekEntry.TimeEntries;

        await IndexedDbAccessor.UpdateValueAsync(WeekEntriesStore, foundWeekEntry);
    }

    public async Task DeleteWeekEntryAsync(WeekEntryModel weekEntry) =>
        await IndexedDbAccessor.DeleteValueAsync(WeekEntriesStore, weekEntry.Id);
}
