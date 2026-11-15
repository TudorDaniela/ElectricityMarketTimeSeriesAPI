using ElectricityMarketTimeSeries.Domain;
using Microsoft.EntityFrameworkCore;

namespace ElectricityMarketTimeSeries.Infrastructure.Data;

public sealed class ElectricityMarketDbContext(DbContextOptions<ElectricityMarketDbContext> options) : DbContext(options)
{
    public DbSet<BiddingZone> BiddingZones => Set<BiddingZone>();
    public DbSet<TimeSeries> TimeSeries => Set<TimeSeries>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElectricityMarketDbContext).Assembly);
    }
}
