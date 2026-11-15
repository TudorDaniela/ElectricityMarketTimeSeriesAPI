namespace ElectricityMarketTimeSeries.Domain;

public class TimeInterval
{
    public required DateTimeOffset PeriodEnd { get; init; }
    public required DateTimeOffset PeriodStart { get; init; }
}
