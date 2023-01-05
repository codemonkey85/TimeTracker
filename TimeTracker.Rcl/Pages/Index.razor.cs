namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private WeekEntryModel? WeekEntryModel { get; set; }
    private DateOnly startDate = DateOnly.FromDateTime(DateTime.Now.StartOfWeek());

    protected override async Task OnInitializedAsync()
    {
        RefreshService.OnChange += StateHasChanged;
        WeekEntryModel = await DataService.GetWeekEntryFromStartDateAsync(startDate) ?? new WeekEntryModel(startDate) { IsNew = true };
        Refresh();
    }

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private async Task OnStartDateChangedAsync()
    {
        startDate = startDate.StartOfWeek();
        WeekEntryModel = await DataService.GetWeekEntryFromStartDateAsync(startDate) ?? new WeekEntryModel(startDate) { IsNew = true };
        Refresh();
    }

    private async Task ExportData()
    {

    }

    private async Task ImportData()
    {

    }

    private async Task ClearAllDataAsync()
    {
        var result = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to clear all data? This cannot be undone.");
        if (result == false)
        {
            return;
        }

        await DataService.ClearAllDataAsync();
        await JsRuntime.InvokeVoidAsync("alert", "All data has been cleared.");

        WeekEntryModel = new WeekEntryModel(startDate) { IsNew = true };

        Refresh();
    }
}
