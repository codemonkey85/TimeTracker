#nullable enable

namespace TimeTracker.MauiBlazor.Services;

public record DataService : IDataService
{
    private List<WeekEntryModel> Data { get; set; } = new();

    private async Task StoreDataAsync() =>
        await SecureStorage.Default.SetAsync(nameof(Data), JsonSerializer.Serialize(Data));

    private async Task LoadDataAsync()
    {
        var storedDataJson = await SecureStorage.Default.GetAsync(nameof(Data));
        if (string.IsNullOrEmpty(storedDataJson))
        {
            return;
        }

        var storedData = JsonSerializer.Deserialize<List<WeekEntryModel>>(storedDataJson);
        if (storedData is null)
        {
            return;
        }

        foreach (var weekEntry in storedData)
        {
            Data.Add(weekEntry);
        }
    }

    public async Task ClearAllDataAsync()
    {
        await LoadDataAsync();
        Data.Clear();
        await StoreDataAsync();
    }

    public async Task CreateWeekEntryAsync(WeekEntryModel weekEntry)
    {
        await LoadDataAsync();
        Data.Add(weekEntry);
        await StoreDataAsync();
    }

    public async Task DeleteWeekEntryAsync(WeekEntryModel weekEntry)
    {
        await LoadDataAsync();
        Data.Remove(weekEntry);
        await StoreDataAsync();
    }

    public async Task<List<IGrouping<int, WeekEntryModel>>?> GetDataGroupedByYearAsync()
    {
        await LoadDataAsync();
        return (await GetAllWeekEntriesAsync())?
            .GroupBy(weekEntry => weekEntry.StartDate.Year)
            .ToList();
    }

    public async Task<List<List<IGrouping<int, WeekEntryModel>>>?> GetDataGroupedByMonthAsync()
    {
        await LoadDataAsync();
        return (await GetAllWeekEntriesAsync())?
            .GroupBy(weekEntry => weekEntry.StartDate.Year)
            .Select(weekEntry => weekEntry
                .GroupBy(w => w.StartDate.Month)
                .ToList()
            ).ToList();
    }

    public async Task ImportDataAsync(ExportImportModel exportImportModel)
    {
        await LoadDataAsync();
        Data.Clear();
        foreach (var weekEntry in exportImportModel.WeekEntries)
        {
            Data.Add(weekEntry);
        }
        await StoreDataAsync();
    }

    public async Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry)
    {
        await LoadDataAsync();
        var indexNum = Data.IndexOf(weekEntry);
        if (indexNum == -1)
        {
            Data.RemoveAt(indexNum);
            Data.Insert(indexNum, weekEntry);
        }
        await StoreDataAsync();
    }

#pragma warning disable IDE0022 // Use expression body for methods
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<ExportImportModel> ExportDataAsync()
    {
        await LoadDataAsync();
        return new ExportImportModel(Data);
    }

    public async Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync()
    {
        await LoadDataAsync();
        return Data;
    }
    public async Task<WeekEntryModel?> GetWeekEntryAsync(int id)
    {
        await LoadDataAsync();
        return Data.FirstOrDefault(weekEntry => weekEntry.Id == id);
    }

    public async Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateTime indexValue)
    {
        await LoadDataAsync();
        return Data.FirstOrDefault(weekEntry => weekEntry.StartDate == indexValue);
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore IDE0022 // Use expression body for methods

}
