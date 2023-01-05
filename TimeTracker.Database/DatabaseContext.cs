namespace TimeTracker.Database;

public class DatabaseContext : DbContext
{
    // https://stackoverflow.com/questions/67966259/how-to-use-sqlite-in-blazor-webassembly

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //}

    private static void SeedData(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<TimeEntryModel>().HasData(
            new TimeEntryModel
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(0),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            },
            new TimeEntryModel
            {
                Id = 2,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            },
            new TimeEntryModel
            {
                Id = 3,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(2),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            },
            new TimeEntryModel
            {
                Id = 4,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            },
            new TimeEntryModel
            {
                Id = 5,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(4),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            },
            new TimeEntryModel
            {
                Id = 6,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(5),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            },
            new TimeEntryModel
            {
                Id = 7,
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(6),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2).AddMinutes(30)),
            });

    public DbSet<TimeEntryModel> TimeEntries { get; set; } = default!;

    public DbSet<WeekEntryModel> WeekEntries { get; set; } = default!;
}
