namespace TimeTracker.Rcl.Components;

public partial class WeekEntry : IDisposable
{
    [Parameter] public WeekEntryModel? WeekEntryModel { get; set; }

    private async Task SaveTimeEntryAsync()
    {

    }

    protected override void OnInitialized() => RefreshService.OnChange += StateHasChanged;

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;
}
