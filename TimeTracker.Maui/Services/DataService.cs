#nullable enable

namespace TimeTracker.Maui.Services;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable IDE0022 // Use expression body for methods
public record DataService : IDataService
{
    private readonly List<WeekEntryModel> data = new();

    public async Task ClearAllDataAsync()
    {
        data.Clear();
    }

    public async Task CreateWeekEntryAsync(WeekEntryModel weekEntry)
    {
        data.Add(weekEntry);
    }

    public async Task DeleteWeekEntryAsync(WeekEntryModel weekEntry)
    {
        data.Remove(weekEntry);
    }

    public async Task<ExportImportModel> ExportDataAsync()
    {
        return new ExportImportModel(data);
    }

    public async Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync()
    {
        return data;
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

    public async Task<WeekEntryModel?> GetWeekEntryAsync(int id)
    {
        return data.FirstOrDefault(weekEntry => weekEntry.Id == id);
    }

    public async Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateOnly indexValue)
    {
        return data.FirstOrDefault(weekEntry => weekEntry.StartDate == indexValue);
    }

    public async Task ImportDataAsync(ExportImportModel exportImportModel)
    {
        data.Clear();
        foreach (var weekEntry in exportImportModel.WeekEntries)
        {
            data.Add(weekEntry);
        }
    }

    public async Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry)
    {
        var indexNum = data.IndexOf(weekEntry);
        if (indexNum == -1)
        {
            data.RemoveAt(indexNum);
            data.Insert(indexNum, weekEntry);
        }
    }
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore IDE0022 // Use expression body for methods
