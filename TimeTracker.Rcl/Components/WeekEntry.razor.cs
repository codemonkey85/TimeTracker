namespace TimeTracker.Rcl.Components;

public partial class WeekEntry : IDisposable
{
    [Parameter] public WeekEntryModel? WeekEntryModel { get; set; }

    private const double MaxHours = 30.5;

    private async Task SaveTimeEntryAsync()
    {
        switch (WeekEntryModel)
        {
            case { IsNew: true }:
                await DataService.CreateWeekEntryAsync(WeekEntryModel);
                break;
            case { IsNew: false }:
                await DataService.UpdateWeekEntryAsync(WeekEntryModel.Id, WeekEntryModel);
                break;
            default:
                return;
        }
    }

    protected override void OnInitialized() => RefreshService.OnChange += StateHasChanged;

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;
}
