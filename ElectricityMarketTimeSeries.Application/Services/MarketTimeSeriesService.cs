using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Responses;

namespace ElectricityMarketTimeSeries.Application.Services;

public class MarketTimeSeriesService : IMarketTimeSeriesService
{
    public async Task<TimeSeriesResponse> GetDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeSeriesResponse> UpdateDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeSeriesResponse> DeleteDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeSeriesResponse> CreateDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
