namespace TimeTracker.MauiBlazor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

        var services = builder.Services;

        services.AddMauiBlazorWebView();
        services
            .AddScoped<TimeTrackerJs>()
            .AddScoped<IDataService, DataService>()
            .AddScoped<ICloudManager, CloudManager>()
            .AddScoped<IRefreshService, RefreshService>()
            .AddScoped<IDialogService, DialogService>();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        return app;

        /*
            var events = new Dictionary<string, Action>();
            Action action = () =>
            {
              // Do stuff when "MySetting" have changed in the cloud
            };
            events.Add("MySetting", action);
            CloudManager.InitChangeTracker(events);
        */
    }
}
