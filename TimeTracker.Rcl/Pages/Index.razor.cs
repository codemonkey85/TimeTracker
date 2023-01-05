namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private WeekEntryModel? WeekEntryModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        RefreshService.OnChange += StateHasChanged;
        var startDate = DateOnly.FromDateTime(DateTime.Now.StartOfWeek());
        WeekEntryModel = await DataService.GetWeekEntryFromStartDateAsync(startDate) ?? new WeekEntryModel(startDate) { IsNew = true };
        Refresh();
    }

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private async Task OnStartDateChangedAsync(ChangeEventArgs e)
    {
        if (WeekEntryModel is null || !DateOnly.TryParse(e.Value?.ToString(), out var startDate))
        {
            return;
        }
        startDate = startDate.StartOfWeek();
        WeekEntryModel = await DataService.GetWeekEntryFromStartDateAsync(startDate) ?? new WeekEntryModel(startDate) { IsNew = true };
        Refresh();
    }
}
