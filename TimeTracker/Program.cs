var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var connectionString = new SqliteConnectionStringBuilder { DataSource = "test.db" }.ToString();

builder.Services
    //.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionStringBuilder.ToString()))
    .AddDbContextFactory<DatabaseContext>(options => options.UseSqlite(connectionString))
    .AddScoped<IDataService, DataService>()
    .AddScoped<IRefreshService, RefreshService>();

var app = builder.Build();

var databaseContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
//databaseContext.Database.Migrate();
await databaseContext.Database.EnsureDeletedAsync();
await databaseContext.Database.EnsureCreatedAsync();

await app.RunAsync();
