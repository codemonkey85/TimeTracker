namespace TimeTracker.Maui.Services;

public record DataService : IDataService
{
    public async Task ClearAllDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateWeekEntryAsync(WeekEntryModel weekEntry)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteWeekEntryAsync(WeekEntryModel weekEntry)
    {
        throw new NotImplementedException();
    }

    public async Task<ExportImportModel> ExportDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<WeekEntryModel>> GetAllWeekEntriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<List<IGrouping<int, WeekEntryModel>>>> GetDataGroupedByMonthAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<IGrouping<int, WeekEntryModel>>> GetDataGroupedByYearAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<WeekEntryModel> GetWeekEntryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<WeekEntryModel> GetWeekEntryFromStartDateAsync(DateOnly indexValue)
    {
        throw new NotImplementedException();
    }

    public async Task ImportDataAsync(ExportImportModel exportImportModel)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry)
    {
        throw new NotImplementedException();
    }
}
