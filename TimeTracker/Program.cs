var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<IndexedDbAccessor>()
    .AddScoped<IDataService, DataService>()
    .AddScoped<IRefreshService, RefreshService>();

var app = builder.Build();

var serviceProvider = app.Services.CreateScope().ServiceProvider;

var indexedDbAccessor = serviceProvider.GetRequiredService<IndexedDbAccessor>();
await indexedDbAccessor.InitializeAsync();

await app.RunAsync();
