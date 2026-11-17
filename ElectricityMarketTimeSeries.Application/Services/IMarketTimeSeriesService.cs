using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Responses;

namespace ElectricityMarketTimeSeries.Application.Services;

public interface IMarketTimeSeriesService
{
    Task<TimeSeriesResponse> GetDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> UpdateDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> DeleteDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> CreateDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken);
}
