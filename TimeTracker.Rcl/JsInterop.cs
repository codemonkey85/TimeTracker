namespace TimeTracker.Rcl;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class JsInterop
{
    [JSImport(nameof(SaveFile), "timetracker.js")]
    public static partial Task SaveFile(string fileName, string fileText);
}
