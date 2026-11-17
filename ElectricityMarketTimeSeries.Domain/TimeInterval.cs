namespace ElectricityMarketTimeSeries.Domain;

public class TimeInterval
{
    public required DateTimeOffset PeriodEnd { get; set; }
    public required DateTimeOffset PeriodStart { get; set; }
}
