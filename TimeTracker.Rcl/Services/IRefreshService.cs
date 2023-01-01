namespace TimeTracker.Rcl.Services;

public interface IRefreshService
{
    event Action? OnChange;

    void Refresh();
}
