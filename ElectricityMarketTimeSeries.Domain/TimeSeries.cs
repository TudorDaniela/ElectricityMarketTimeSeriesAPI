namespace ElectricityMarketTimeSeries.Domain;

public sealed class TimeSeries
{
    public required Guid Id { get; init; }
    public required TimeSeriesType Type { get; set; }
    public required Guid SourceBiddingZoneId { get; set; }
    public required BiddingZone SourceBiddingZone { get; set; }
    public Guid? TargetBiddingZoneId { get; set; }
    public BiddingZone? TargetBiddingZone { get; set; }
    public required MeasurementUnit MeasurementUnit { get; set; }
    public required Period Period { get; set; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; set; }
}
