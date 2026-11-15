namespace ElectricityMarketTimeSeries.Domain;

public class Period
{
    public required List<TimeSeriesPoint> Points { get; init; }
    public required Resolution Resolution { get; init; }
    public required TimeInterval TimeInterval { get; init; }
}
