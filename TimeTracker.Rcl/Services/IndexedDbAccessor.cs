namespace TimeTracker.Rcl.Services;

public class IndexedDbAccessor : IAsyncDisposable
{
    private Lazy<IJSObjectReference> accessorJsRef = new();
    private readonly IJSRuntime jsRuntime;

    public IndexedDbAccessor(IJSRuntime jsRuntime) => this.jsRuntime = jsRuntime;

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await accessorJsRef.Value.InvokeVoidAsync("initialize");
    }

    private async Task WaitForReference()
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

    public async Task<T> GetValueAsync<T>(string storeName, int id)
    {
        await WaitForReference();
        var result = await accessorJsRef.Value.InvokeAsync<T>("get", storeName, id);

        return result;
    }

    public async Task<List<T>> GetAllValuesAsync<T>(string storeName)
    {
        await WaitForReference();
        var result = await accessorJsRef.Value.InvokeAsync<List<T>>("getAll", storeName);

        return result;
    }

    public async Task SetValueAsync<T>(string storeName, T value)
    {
        await WaitForReference();
        await accessorJsRef.Value.InvokeVoidAsync("set", storeName, value);
    }

    public async Task RemoveValueAsync(string storeName, int id)
    {
        await WaitForReference();
        await accessorJsRef.Value.InvokeVoidAsync("remove", storeName, id);
    }
}
