using DialogService = TimeTracker.Services.DialogService;
using IDialogService = TimeTracker.Rcl.Services.IDialogService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddMudServices()
    .AddLogging()
    .AddScoped<TimeTrackerJs>()
    .AddScoped<IndexedDbAccessor>()
    .AddScoped<IDataService, DataService>()
    .AddScoped<ICloudManager, CloudManager>()
    .AddScoped<IRefreshService, RefreshService>()
    .AddScoped<IDialogService, DialogService>();

var app = builder.Build();

var serviceProvider = app.Services.CreateScope().ServiceProvider;

var indexedDbAccessor = serviceProvider.GetRequiredService<IndexedDbAccessor>();
await indexedDbAccessor.InitializeAsync();

await app.RunAsync();
