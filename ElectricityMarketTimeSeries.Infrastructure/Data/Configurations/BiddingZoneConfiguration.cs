using ElectricityMarketTimeSeries.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectricityMarketTimeSeries.Infrastructure.Data.Configurations;

public sealed class BiddingZoneConfiguration : IEntityTypeConfiguration<BiddingZone>
{
    public void Configure(EntityTypeBuilder<BiddingZone> builder)
    {
        builder.ToTable("BiddingZones");

        builder.HasKey(zone => zone.Id);

        builder.Property(zone => zone.Name)
            .IsRequired();

        builder.Property(zone => zone.EicCode)
            .IsRequired();

        builder.HasIndex(zone => zone.EicCode)
            .IsUnique();
    }
}
