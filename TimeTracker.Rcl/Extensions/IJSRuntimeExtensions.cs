namespace TimeTracker.Rcl.Extensions;

// ReSharper disable once InconsistentNaming
public static class IJSRuntimeExtensions
{
    public static async Task<bool> Confirm(this IJSRuntime jSRuntime, string message) =>
        await jSRuntime.InvokeAsync<bool>("confirm", message);

    public static async Task Alert(this IJSRuntime jSRuntime, string message) =>
        await jSRuntime.InvokeVoidAsync("alert", message);

    public static async Task<IJSObjectReference> Import(this IJSRuntime jSRuntime, string path) =>
        await jSRuntime.InvokeAsync<IJSObjectReference>("import", path);
}
