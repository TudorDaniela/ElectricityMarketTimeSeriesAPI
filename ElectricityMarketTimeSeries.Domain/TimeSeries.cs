namespace ElectricityMarketTimeSeries.Domain;

public sealed class TimeSeries
{
    public required Guid Id { get; init; }
    public required TimeSeriesType Type { get; init; }
    public required BiddingZone SourceBiddingZone { get; init; }
    public BiddingZone? TargetBiddingZone { get; init; }
    public required MeasurementUnit MeasurementUnit { get; init; }
    public required Period Period { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
}
