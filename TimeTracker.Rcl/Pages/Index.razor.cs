namespace TimeTracker.Rcl.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Index : IDisposable
{
    private WeekEntryModel? WeekEntryModel { get; set; }
    private DateTime? startDate = DateTime.Now.StartOfWeek();
    private IBrowserFile? browserFile;

    protected override async Task OnInitializedAsync()
    {
        RefreshService.OnChange += StateHasChanged;
        WeekEntryModel = await DataService.GetWeekEntryFromStartDateAsync(startDate) ?? new WeekEntryModel(startDate) { IsNew = true };
        Refresh();
    }

    public void Dispose() => RefreshService.OnChange -= StateHasChanged;

    private void Refresh() => RefreshService.Refresh();

    private void HandleFile(IBrowserFile file) => browserFile = file;

    private async Task OnStartDateChangedAsync()
    {
        startDate = startDate?.StartOfWeek();
        WeekEntryModel = await DataService.GetWeekEntryFromStartDateAsync(startDate) ?? new WeekEntryModel(startDate) { IsNew = true };
        Refresh();
    }

    private async Task ExportData()
    {
        var data = await DataService.ExportDataAsync();
        var json = JsonSerializer.Serialize(data);
        var fileName = $"TimeTrackerExport-{DateTime.Now:yyyyMMddHHmmssfff}.json";

        try
        {
            await TimeTrackerJs.SaveFile(fileName, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task ImportData()
    {
        if (browserFile is null)
        {
            return;
        }

        await using var fileStream = browserFile.OpenReadStream();
        using var streamReader = new StreamReader(fileStream);
        var json = await streamReader.ReadToEndAsync();
        var data = JsonSerializer.Deserialize<ExportImportModel>(json);
        if (data is null)
        {
            return;
        }

        var result = await DialogService.ConfirmAsync(
            "Import Data",
            "Are you sure you want to import data? The imported data will overwrite the current data. This cannot be undone.",
            "Yes",
            "No");
        if (result == false)
        {
            return;
        }

        await DataService.ClearAllDataAsync();
        await DataService.ImportDataAsync(data);
        await DialogService.AlertAsync(
            "Import Data",
            "Data has been imported and has overwritten current data.",
            "Ok");

        startDate = DateTime.Now.StartOfWeek();
        await OnStartDateChangedAsync();
    }

    private async Task ClearAllDataAsync()
    {
        var result = await DialogService.ConfirmAsync(
            "Clear Data",
            "Are you sure you want to clear all data? This cannot be undone.",
            "Yes",
            "No");
        if (result == false)
        {
            return;
        }

        await DataService.ClearAllDataAsync();
        await DialogService.AlertAsync(
            "Clear Data",
            "All data has been cleared.",
            "Ok");

        startDate = DateTime.Now.StartOfWeek();
        await OnStartDateChangedAsync();
    }
}
