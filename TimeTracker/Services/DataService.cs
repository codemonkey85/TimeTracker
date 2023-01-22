namespace TimeTracker.Services;

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

    public async Task<ExportImportModel> ExportDataAsync()
    {
        var result = new ExportImportModel(await IndexedDbAccessor.GetAllValuesAsync<WeekEntryModel>(Constants.WeekEntriesStore));
        return result;
    }

    public async Task ImportDataAsync(ExportImportModel exportImportModel)
    {
        foreach (var weekEntry in exportImportModel.WeekEntries)
        {
            await IndexedDbAccessor.CreateValueAsync(Constants.WeekEntriesStore, weekEntry);
        }
    }

    public async Task<List<IGrouping<int, WeekEntryModel>>?> GetDataGroupedByYearAsync() =>
        (await GetAllWeekEntriesAsync())?
            .GroupBy(weekEntry => weekEntry.StartDate.Year)
            .ToList();

    public async Task<List<List<IGrouping<int, WeekEntryModel>>>?> GetDataGroupedByMonthAsync() =>
        (await GetAllWeekEntriesAsync())?
            .GroupBy(weekEntry => weekEntry.StartDate.Year)
            .Select(weekEntry => weekEntry
                .GroupBy(w => w.StartDate.Month)
                .ToList()
            ).ToList();
}
