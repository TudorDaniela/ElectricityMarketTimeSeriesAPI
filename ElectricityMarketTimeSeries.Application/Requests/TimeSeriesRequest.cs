namespace ElectricityMarketTimeSeries.Application.Requests;

public record TimeSeriesRequest(Guid BiddingZoneId, DateTimeOffset StartDate, DateTimeOffset EndDate);