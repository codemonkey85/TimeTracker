namespace TimeTracker.Rcl.Components;

public partial class TimeEntry : IDisposable
{
    [Parameter] public TimeEntryModel? TimeEntryModel { get; set; }

    protected override void OnInitialized() => RefreshService.OnChange += StateHasChanged;

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private void OnStartTimeChanged(ChangeEventArgs e)
    {
        if (TimeEntryModel is null || !TimeOnly.TryParse(e.Value?.ToString(), out var startTime))
        {
            return;
        }

        TimeEntryModel.StartTime = startTime;
        Refresh();
    }

    private void OnEndTimeChanged(ChangeEventArgs e)
    {
        if (TimeEntryModel is null || !TimeOnly.TryParse(e.Value?.ToString(), out var endTime))
        {
            return;
        }

        TimeEntryModel.EndTime = endTime;
        Refresh();
    }
}
