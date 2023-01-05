namespace TimeTracker.Rcl.Services;

public record DataService(IndexedDbAccessor IndexedDbAccessor) : IDataService
{
    public async Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateOnly indexValue) =>
        await IndexedDbAccessor.GetValueByIndexAsync<WeekEntryModel>(Constants.WeekEntriesStore, "startDate", indexValue);

    public async Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync() =>
        await IndexedDbAccessor.GetAllValuesAsync<WeekEntryModel>(Constants.WeekEntriesStore);

    public async Task<WeekEntryModel?> GetWeekEntryAsync(int id) =>
        await IndexedDbAccessor.GetValueAsync<WeekEntryModel>(Constants.WeekEntriesStore, id);

    public async Task CreateWeekEntryAsync(WeekEntryModel weekEntry) =>
        await IndexedDbAccessor.CreateValueAsync(Constants.WeekEntriesStore, weekEntry);

    public async Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry)
    {
        var foundWeekEntry = await GetWeekEntryAsync(id);
        if (foundWeekEntry is null)
        {
            return;
        }

        foundWeekEntry.TimeEntries = weekEntry.TimeEntries;

        await IndexedDbAccessor.UpdateValueAsync(Constants.WeekEntriesStore, foundWeekEntry);
    }

    public async Task DeleteWeekEntryAsync(WeekEntryModel weekEntry) =>
        await IndexedDbAccessor.DeleteValueAsync(Constants.WeekEntriesStore, weekEntry.Id);

    public async Task ClearAllDataAsync() =>
        await IndexedDbAccessor.ClearAllDataAsync(Constants.WeekEntriesStore);
}
