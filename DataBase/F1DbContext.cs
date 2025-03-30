using DataBase.Models;
using Microsoft.EntityFrameworkCore;


namespace DataBase;

public class F1DbContext : DbContext
{
    private const string Schema = "f1";
    private const string RacesResultsTableName = "RacesResults";
    private const string DriversTableName = "Drivers";
    
    public DbSet<RaceResultEntity> RaceResults { get; set; }
    public DbSet<DriversEntity> Drivers { get; set; }
    
    public F1DbContext(DbContextOptions<F1DbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.Entity<RaceResultEntity>().ToTable(RacesResultsTableName);
        modelBuilder.Entity<RaceResultEntity>().HasKey(k => new { k.Year, k.Round, k.DriverId });
        modelBuilder.Entity<RaceResultEntity>().Property(p => p.FastLap).IsRequired(false);

        modelBuilder.Entity<DriversEntity>().ToTable(DriversTableName);
        modelBuilder.Entity<DriversEntity>().HasKey(k => new { k.Year, k.DriverId });
    }

    public async Task UpsertRacesAsync(IEnumerable<RaceResultEntity> races)
    {
        await RaceResults.UpsertRange(races).On(r => new { r.Year, r.Round, r.DriverId }).RunAsync();
    }

    public async Task UpsertDriversAsync(IEnumerable<DriversEntity> drivers)
    {
        await Drivers.UpsertRange(drivers).On(d => new { d.Year, d.DriverId }).RunAsync();
    }
}