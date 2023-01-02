namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private DateTime StartDate { get; set; }

    private WeekEntryModel? WeekEntryModel { get; set; }

    protected override void OnInitialized()
    {
        StartDate = DateTime.Now;
        WeekEntryModel = new WeekEntryModel(StartDate);
        RefreshService.OnChange += StateHasChanged;
    }

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void AfterStartDateChanged() => WeekEntryModel = new WeekEntryModel(StartDate);
}
