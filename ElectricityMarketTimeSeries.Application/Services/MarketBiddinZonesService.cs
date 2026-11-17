using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Responses;

namespace ElectricityMarketTimeSeries.Application.Services;

public class MarketBiddingZonesService : IMarketBiddingZonesService
{
    public async Task<BiddingZoneResponse> GetDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BiddingZoneResponse> UpdateDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BiddingZoneResponse> DeleteDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BiddingZoneResponse> CreateDataAsync(BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
