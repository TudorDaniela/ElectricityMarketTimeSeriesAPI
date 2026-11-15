using ElectricityMarketTimeSeries.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectricityMarketTimeSeries.Infrastructure.Data.Configurations;

public class TimeSeriesConfiguration : IEntityTypeConfiguration<TimeSeries>
{
    public void Configure(EntityTypeBuilder<TimeSeries> builder)
    {
        builder.ToTable("TimeSeries");

        builder.HasKey(series => series.Id);

        builder.Property(series => series.Type)
            .HasConversion<string>();

        builder.Property(series => series.MeasurementUnit)
            .HasConversion<string>();

        builder.HasOne(series => series.SourceBiddingZone)
            .WithMany()
            .HasForeignKey("SourceBiddingZoneId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(series => series.TargetBiddingZone)
            .WithMany()
            .HasForeignKey("TargetBiddingZoneId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(series => series.Period, period =>
        {
            period.Property(p => p.Resolution)
                .HasColumnName("Resolution")
                .HasConversion<string>();

            period.OwnsOne(p => p.TimeInterval, interval =>
            {
                interval.Property(i => i.PeriodStart)
                    .HasColumnName("PeriodStartUtcTicks")
                    .HasConversion(
                        instant => instant.UtcTicks,
                        ticks => new DateTimeOffset(ticks, TimeSpan.Zero));

                interval.Property(i => i.PeriodEnd)
                    .HasColumnName("PeriodEndUtcTicks")
                    .HasConversion(
                        instant => instant.UtcTicks,
                        ticks => new DateTimeOffset(ticks, TimeSpan.Zero));
            });

            period.Navigation(p => p.TimeInterval).IsRequired();
            period.OwnsMany(p => p.Points, point =>
            {
                point.ToTable("TimeSeriesPoints");

                point.WithOwner()
                    .HasForeignKey("TimeSeriesId");

                point.Property(p => p.Position)
                    .ValueGeneratedNever();

                point.HasKey("TimeSeriesId", nameof(TimeSeriesPoint.Position));

                point.Property(p => p.Quantity)
                    .IsRequired();
            });
        });

        builder.Navigation(series => series.Period).IsRequired();
    }
}
