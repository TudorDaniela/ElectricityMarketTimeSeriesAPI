using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Responses;

namespace ElectricityMarketTimeSeries.Application.Services;

public interface IMarketBiddingZonesService
{
    Task<BiddingZoneResponse> GetDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken);
    Task<BiddingZoneResponse> UpdateDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken);
    Task<BiddingZoneResponse> DeleteDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken);
    Task<BiddingZoneResponse> CreateDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken);
}