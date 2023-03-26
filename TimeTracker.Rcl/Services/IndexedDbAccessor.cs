namespace TimeTracker.Rcl.Services;

public class IndexedDbAccessor : JsModule
{
    public IndexedDbAccessor(IJSRuntime js)
        : base(js, "/_content/TimeTracker.Rcl/js/IndexedDbAccessor.js")
    {
    }

    public async Task InitializeAsync() =>
        await InvokeVoidAsync("initialize");

    public async Task<T> GetValueByIndexAsync<T>(string storeName, string indexName, object? indexValue) =>
        await InvokeAsync<T>("getByIndex", storeName, indexName, indexValue ?? "");

    public async Task<List<T>> GetAllValuesAsync<T>(string storeName) =>
        await InvokeAsync<List<T>>("getAll", storeName);

    public async Task<T> GetValueAsync<T>(string storeName, int id) =>
        await InvokeAsync<T>("get", storeName, id);

    public async Task CreateValueAsync<T>(string storeName, T value) where T : notnull =>
        await InvokeVoidAsync("add", storeName, value);

    public async Task UpdateValueAsync<T>(string storeName, T value) where T : notnull =>
        await InvokeVoidAsync("put", storeName, value);

    public async Task DeleteValueAsync(string storeName, int id) =>
        await InvokeVoidAsync("remove", storeName, id);

    public async Task ClearAllDataAsync(string storeName) =>
        await InvokeVoidAsync("clear", storeName);
}
