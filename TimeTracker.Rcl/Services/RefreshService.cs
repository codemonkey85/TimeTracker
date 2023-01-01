namespace TimeTracker.Rcl.Services;

public class RefreshService : IRefreshService
{
    public event Action? OnChange;

    public void Refresh() => OnChange?.Invoke();
}
