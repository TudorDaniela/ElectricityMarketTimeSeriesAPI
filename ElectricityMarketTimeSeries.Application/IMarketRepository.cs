using ElectricityMarketTimeSeries.Domain;

namespace ElectricityMarketTimeSeries.Application;

public interface ITimeSeriesRepository
{
    Task<IReadOnlyList<TimeSeries>> FindAsync(
        Guid biddingZoneId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken);
    Task<TimeSeries?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BiddingZone?> FindBiddingZoneAsync(Guid id, CancellationToken cancellationToken);
    Task<TimeSeries> CreateAsync(TimeSeries timeSeries, CancellationToken cancellationToken);
    Task<TimeSeries?> UpdateAsync(TimeSeries timeSeries, CancellationToken cancellationToken);
    Task<TimeSeries?> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
