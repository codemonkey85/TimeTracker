namespace TimeTracker.Rcl.Components;

public partial class WeekEntry : IDisposable
{
    [Parameter] public WeekEntryModel? WeekEntryModel { get; set; }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable CA1822 // Mark members as static
    private async Task SaveTimeEntryAsync()
#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {

    }

    protected override void OnInitialized() => RefreshService.OnChange += StateHasChanged;

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;
}
