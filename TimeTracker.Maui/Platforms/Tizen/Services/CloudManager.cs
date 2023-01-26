namespace TimeTracker.MauiBlazor.Services;

public record CloudManager : ICloudManager
{
    public void InitChangeTracker(Dictionary<string, Action> events)
    {
        throw new NotImplementedException();
    }

    public string LoadString(string name)
    {
        throw new NotImplementedException();
    }

    public bool SaveString(string name, string value)
    {
        throw new NotImplementedException();
    }
}
