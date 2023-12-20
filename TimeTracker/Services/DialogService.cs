using IDialogService = TimeTracker.Rcl.Services.IDialogService;

namespace TimeTracker.Services;

public record DialogService(IJSRuntime JsRuntime) : IDialogService
{
    public async Task<bool> ConfirmAsync(string title, string message, string accept, string cancel) =>
        await JsRuntime.ConfirmAsync(message);

    public async Task AlertAsync(string title, string message, string cancel) =>
        await JsRuntime.AlertAsync(message);
}
