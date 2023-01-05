namespace TimeTracker.Rcl.Services;

public class IndexedDbAccessor : IAsyncDisposable
{
    public Lazy<IJSObjectReference> accessorJsRef = new();
    public readonly IJSRuntime jsRuntime;

    public IndexedDbAccessor(IJSRuntime jsRuntime) => this.jsRuntime = jsRuntime;

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await accessorJsRef.Value.InvokeVoidAsync("initialize");
    }

    public async Task WaitForReference()
    {
        if (accessorJsRef.IsValueCreated is false)
        {
            accessorJsRef = new Lazy<IJSObjectReference>(
                await jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "/_content/TimeTracker.Rcl/js/IndexedDbAccessor.js"));
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (accessorJsRef.IsValueCreated)
        {
            await accessorJsRef.Value.DisposeAsync();
        }
    }
}
