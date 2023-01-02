namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private WeekEntryModel? WeekEntryModel { get; set; }

    protected override void OnInitialized()
    {
        WeekEntryModel = new WeekEntryModel(DateTime.Now);
        RefreshService.OnChange += StateHasChanged;
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
}
