namespace TimeTracker.Rcl.Services;

public interface IDataService
{
    Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateOnly indexValue);

    Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync();

    Task<WeekEntryModel?> GetWeekEntryAsync(int id);

    Task CreateWeekEntryAsync(WeekEntryModel weekEntry);

    Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry);

    Task DeleteWeekEntryAsync(WeekEntryModel weekEntry);

    Task ClearAllDataAsync();

    Task<ExportImportModel> ExportDataAsync();

    Task ImportDataAsync(ExportImportModel exportImportModel);
}
