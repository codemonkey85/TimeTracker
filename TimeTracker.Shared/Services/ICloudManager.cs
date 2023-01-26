namespace TimeTracker.Shared.Services;

public interface ICloudManager
{
    void InitChangeTracker(Dictionary<string, Action> events);
    bool SaveString(string name, string value);
    string LoadString(string name);
}
