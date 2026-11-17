namespace ElectricityMarketTimeSeries.Application.Requests;

public class TimeSeriesRequest
{
    public required int BiddingZoneId { get; init; }
    public required DateTimeOffset StartDate { get; init; }
    public required DateTimeOffset EndDate { get; init; }
}
