using DataBase.Models;
using Microsoft.EntityFrameworkCore;


namespace DataBase;

public class F1DbContext : DbContext
{
    private const string Schema = "f1";
    private const string RacesResultsTableName = "RacesResults";
    
    public DbSet<RaceResultEntity> RaceResults { get; set; }
    
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

    }

    public async Task UpsertRacesAsync(IEnumerable<RaceResultEntity> races)
    {
        await RaceResults.UpsertRange(races).On(r => new { r.Year, r.Round, r.DriverId }).RunAsync();
    }
    
}