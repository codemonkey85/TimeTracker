var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var connectionString = new SqliteConnectionStringBuilder { DataSource = "test.db" }.ToString();

builder.Services
    //.AddDbContextFactory<DatabaseContext>(options => options.UseSqlite(connectionString))
    .AddScoped<IndexedDbAccessor>()
    .AddScoped<IDataService, DataService>()
    .AddScoped<IRefreshService, RefreshService>();

var app = builder.Build();

//var databaseContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
//await databaseContext.Database.EnsureCreatedAsync();

var dataService = app.Services.CreateScope().ServiceProvider.GetRequiredService<IDataService>();
var timeEntries = new List<TimeEntryModel>
{
    new()
    {
        Id = 1,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(0),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    },
    new()
    {
        Id = 2,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    },
    new()
    {
        Id = 3,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(2),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    },
    new()
    {
        Id = 4,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    },
    new()
    {
        Id = 5,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(4),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    },
    new()
    {
        Id = 6,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(5),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    },
    new()
    {
        Id = 7,
        Date = DateOnly.FromDateTime(DateTime.Now).AddDays(6),
        StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
    }
};

foreach (var timeEntry in timeEntries)
{
    await dataService.CreateTimeEntry(timeEntry);
}

await app.RunAsync();
