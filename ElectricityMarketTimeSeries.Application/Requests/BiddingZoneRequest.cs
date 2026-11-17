namespace ElectricityMarketTimeSeries.Application.Requests;

public record BiddingZoneRequest(Guid Id, DateTimeOffset StartDate);
