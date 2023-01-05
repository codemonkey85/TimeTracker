namespace TimeTracker.Rcl.Services;

public record DataService(DatabaseContext DatabaseContext) : IDataService
{
    public async Task<List<TimeEntryModel>> TestAsync()
    {
        return await DatabaseContext.TimeEntries.ToListAsync();
    }
}
