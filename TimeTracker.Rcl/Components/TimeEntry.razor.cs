namespace TimeTracker.Rcl.Components;

public partial class TimeEntry : IDisposable
{
    [Parameter] public TimeEntryModel? TimeEntryModel { get; set; }

    protected override void OnInitialized() => RefreshService.OnChange += StateHasChanged;

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private void OnStartTimeChanged() => Refresh();

    private void OnEndTimeChanged() => Refresh();
}
