namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private WeekEntryModel? WeekEntryModel { get; set; }

    private List<TimeEntryModel>? TestTimeEntries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        WeekEntryModel = new WeekEntryModel(DateTime.Now);
        RefreshService.OnChange += StateHasChanged;

        TestTimeEntries = await DataService.GetTimeEntries();
    }

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private void OnStartDateChanged(ChangeEventArgs e)
    {
        if (WeekEntryModel is null || !DateOnly.TryParse(e.Value?.ToString(), out var startDate))
        {
            return;
        }

        WeekEntryModel = new WeekEntryModel(startDate);
        Refresh();
    }

    private async Task DeleteTimeEntryAsync(TimeEntryModel timeEntry)
    {
        await DataService.DeleteTimeEntry(timeEntry);
        TestTimeEntries = await DataService.GetTimeEntries();
        //RefreshService.Refresh();
    }
}
