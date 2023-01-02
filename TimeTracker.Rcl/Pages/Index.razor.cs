namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private WeekEntryModel? WeekEntryModel { get; set; }

    protected override void OnInitialized()
    {
        WeekEntryModel = new WeekEntryModel(DateTime.Now);
        RefreshService.OnChange += StateHasChanged;
        Refresh();
    }

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private void OnStartDateChanged()
    {
        if (WeekEntryModel is null)
        {
            return;
        }

        WeekEntryModel = new WeekEntryModel(WeekEntryModel.StartDate);
        Refresh();
    }
}
