using ElectricityMarketTimeSeries.Application;
using ElectricityMarketTimeSeries.Application.Exceptions;
using ElectricityMarketTimeSeries.Domain;
using Microsoft.EntityFrameworkCore;

namespace ElectricityMarketTimeSeries.Infrastructure.Data;

public sealed class TimeSeriesRepository(ElectricityMarketDbContext dbContext) : ITimeSeriesRepository
{
    public async Task<IReadOnlyList<TimeSeries>> FindAsync(
        Guid biddingZoneId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken)
    {
        var result = await dbContext.TimeSeries.ToListAsync(cancellationToken);
        return result;
        return await WithZones(dbContext.TimeSeries.AsNoTracking())
            .Where(timeSeries => timeSeries.CreatedAt >= from.UtcDateTime
                && timeSeries.CreatedAt <= to.UtcDateTime
                && timeSeries.SourceBiddingZoneId == biddingZoneId)
            .ToListAsync(cancellationToken);
    }

    public async Task<TimeSeries?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await WithZones(dbContext.TimeSeries.AsNoTracking())
            .SingleOrDefaultAsync(timeSeries => timeSeries.Id == id, cancellationToken);
    }

    public async Task<BiddingZone?> FindBiddingZoneAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.BiddingZones.AsNoTracking()
            .SingleOrDefaultAsync(zone => zone.Id == id, cancellationToken);
    }

    public async Task<TimeSeries> CreateAsync(TimeSeries timeSeries, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        timeSeries.SourceBiddingZone = await FindTrackedZoneAsync(timeSeries.SourceBiddingZoneId, cancellationToken)
            ?? throw new NotFoundException($"Source bidding zone '{timeSeries.SourceBiddingZoneId}' was not found.");
        timeSeries.TargetBiddingZone = timeSeries.TargetBiddingZoneId is { } targetZoneId
            ? await FindTrackedZoneAsync(targetZoneId, cancellationToken)
                ?? throw new NotFoundException($"Target bidding zone '{targetZoneId}' was not found.")
            : null;

        dbContext.TimeSeries.Add(timeSeries);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return timeSeries;
    }

    public async Task<TimeSeries?> UpdateAsync(TimeSeries updatedTimeSeries, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        var existing = await WithZones(dbContext.TimeSeries)
            .SingleOrDefaultAsync(timeSeries => timeSeries.Id == updatedTimeSeries.Id, cancellationToken);

        if (existing is null)
        {
            return null;
        }

        var sourceZone = await FindTrackedZoneAsync(updatedTimeSeries.SourceBiddingZoneId, cancellationToken)
            ?? throw new NotFoundException($"Source bidding zone '{updatedTimeSeries.SourceBiddingZoneId}' was not found.");
        var targetZone = updatedTimeSeries.TargetBiddingZoneId is { } targetZoneId
            ? await FindTrackedZoneAsync(targetZoneId, cancellationToken)
                ?? throw new NotFoundException($"Target bidding zone '{targetZoneId}' was not found.")
            : null;

        existing.Type = updatedTimeSeries.Type;
        existing.SourceBiddingZoneId = updatedTimeSeries.SourceBiddingZoneId;
        existing.SourceBiddingZone = sourceZone;
        existing.TargetBiddingZoneId = updatedTimeSeries.TargetBiddingZoneId;
        existing.TargetBiddingZone = targetZone;
        existing.MeasurementUnit = updatedTimeSeries.MeasurementUnit;
        existing.Period.Resolution = updatedTimeSeries.Period.Resolution;
        existing.Period.TimeInterval.PeriodStart = updatedTimeSeries.Period.TimeInterval.PeriodStart;
        existing.Period.TimeInterval.PeriodEnd = updatedTimeSeries.Period.TimeInterval.PeriodEnd;
        existing.UpdatedAt = updatedTimeSeries.UpdatedAt;
        existing.Period.Points.Clear();

        await dbContext.SaveChangesAsync(cancellationToken);
        existing.Period.Points.AddRange(updatedTimeSeries.Period.Points);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return existing;
    }

    public async Task<TimeSeries?> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeSeries = await WithZones(dbContext.TimeSeries)
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (timeSeries is null)
        {
            return null;
        }

        dbContext.TimeSeries.Remove(timeSeries);
        await dbContext.SaveChangesAsync(cancellationToken);
        return timeSeries;
    }

    private async Task<BiddingZone?> FindTrackedZoneAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.BiddingZones.Local.SingleOrDefault(zone => zone.Id == id)
            ?? await dbContext.BiddingZones.SingleOrDefaultAsync(zone => zone.Id == id, cancellationToken);
    }

    private static IQueryable<TimeSeries> WithZones(IQueryable<TimeSeries> query)
    {
        return query
            .Include(timeSeries => timeSeries.SourceBiddingZone)
            .Include(timeSeries => timeSeries.TargetBiddingZone);
    }
}
