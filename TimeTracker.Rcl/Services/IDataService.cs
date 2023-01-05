namespace TimeTracker.Rcl.Services;

public interface IDataService
{
    Task<List<TimeEntryModel>?> GetAllTimeEntriesAsync();

    Task<TimeEntryModel?> GetTimeEntryAsync(int id);

    Task CreateTimeEntryAsync(TimeEntryModel timeEntry);

    Task UpdateTimeEntryAsync(int id, TimeEntryModel timeEntry);

    Task DeleteTimeEntryAsync(TimeEntryModel timeEntry);

    Task<WeekEntryModel?> GetWeekEntryFromStartDateAsync(DateOnly indexValue);

    Task<List<WeekEntryModel>?> GetAllWeekEntriesAsync();

    Task<WeekEntryModel?> GetWeekEntryAsync(int id);

    Task CreateWeekEntryAsync(WeekEntryModel weekEntry);

    Task UpdateWeekEntryAsync(int id, WeekEntryModel weekEntry);

    Task DeleteWeekEntryAsync(WeekEntryModel weekEntry);

}
