namespace TimeTracker.Rcl.Services;

public interface IDataService
{
    Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateTime? indexValue);

    Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync();

    Task<WeekEntryModel?> GetWeekEntryAsync(int id);

    Task CreateWeekEntryAsync(WeekEntryModel weekEntry);

    Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry);

    Task DeleteWeekEntryAsync(WeekEntryModel weekEntry);

    Task ClearAllDataAsync();

    Task<ExportImportModel> ExportDataAsync();

    Task ImportDataAsync(ExportImportModel exportImportModel);

    Task<List<IGrouping<int, WeekEntryModel>>?> GetDataGroupedByYearAsync();

    Task<List<List<IGrouping<int, WeekEntryModel>>>?> GetDataGroupedByMonthAsync();
}
