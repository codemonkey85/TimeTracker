namespace TimeTracker.Rcl.Services;

public interface IDataService
{
    Task<List<TimeEntryModel>?> GetAllTimeEntries();

    Task<TimeEntryModel?> GetTimeEntry(int id);

    Task CreateTimeEntry(TimeEntryModel timeEntry);

    Task UpdateTimeEntry(int id, TimeEntryModel timeEntry);

    Task DeleteTimeEntry(TimeEntryModel timeEntry);

    Task<List<WeekEntryModel>?> GetAllWeekEntries();

    Task<WeekEntryModel?> GetWeekEntry(int id);

    Task CreateWeekEntry(WeekEntryModel weekEntry);

    Task UpdateWeekEntry(int id, WeekEntryModel weekEntry);

    Task DeleteWeekEntry(WeekEntryModel weekEntry);

}
