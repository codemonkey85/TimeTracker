using Foundation;

namespace TimeTracker.MauiBlazor.Services;

public record CloudManager(ILogger Logger) : ICloudManager
{
    private readonly NSUbiquitousKeyValueStore _store = NSUbiquitousKeyValueStore.DefaultStore;

    public void InitChangeTracker(Dictionary<string, Action> events)
    {
        if (events is not null)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(
                NSUbiquitousKeyValueStore.DidChangeExternallyNotification);
        }

        if (events is null || !events.Any())
        {
            return;
        }

        NSNotificationCenter.DefaultCenter.AddObserver(
            NSUbiquitousKeyValueStore.DidChangeExternallyNotification,
            notification => ValuesChanged(notification, events));
    }

    public bool SaveString(string name, string value)
    {
        try
        {
            _store.SetString(name, value);
            var result = _store.Synchronize(); // Runs async in background
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Logger.LogError(e, "{className}", nameof(CloudManager));
            return false;
        }
    }

    public string LoadString(string name)
    {
        try
        {
            var setting = _store.GetString(name);
            return setting;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Logger.LogError(e, "{className}", nameof(CloudManager));
            return null;
        }
    }

    private void ValuesChanged(NSNotification notification, Dictionary<string, Action> events)
    {
        var userInfo = notification.UserInfo;
        if (userInfo is null)
        {
            return;
        }
        var reasonNumber = (NSNumber)userInfo.ObjectForKey(NSUbiquitousKeyValueStore.ChangeReasonKey);
        var reason = reasonNumber.NIntValue;

        // Max 64kb for the whole storage
        if (reason == 2)
        {
            Logger.LogError(new Exception("Cloud QuotaViolationChange"), "{className}", nameof(CloudManager));
            return;
        }

        var changedKeys = (NSArray)userInfo.ObjectForKey(NSUbiquitousKeyValueStore.ChangedKeysKey);
        var changedKeysList = new List<string>();
        for (uint i = 0; i < changedKeys.Count; i++)
        {
            var key = changedKeys.GetItem<NSString>(i); // resolve key to a string
            changedKeysList.Add(key);
        }

        foreach (var key in changedKeysList)
        {
            events.TryGetValue(key, out var action);
            action?.Invoke();
        }
    }
}
