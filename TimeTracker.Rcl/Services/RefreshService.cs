namespace TimeTracker.Rcl.Services;

public record RefreshService : IRefreshService
{
    public event Action? OnChange;

    public void Refresh() => OnChange?.Invoke();
}
