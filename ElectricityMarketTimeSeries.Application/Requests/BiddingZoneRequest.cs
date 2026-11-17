namespace ElectricityMarketTimeSeries.Application.Requests;

public class BiddingZoneRequest
{
    public required Guid Id { get; init; }
    DateTimeOffset StartDate { get; init; }
}
