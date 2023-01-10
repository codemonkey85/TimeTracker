namespace TimeTracker.Rcl.Interop;

public class TimeTrackerJs : JsModule
{
    public TimeTrackerJs(IJSRuntime js)
        : base(js, $"../_content/{typeof(App).Namespace}/{nameof(Pages)}/{nameof(Index)}.razor.js")
    {
    }

    public async ValueTask SaveFile(string fileName, string fileText) =>
        await InvokeVoidAsync(nameof(SaveFile), fileName, fileText);
}
