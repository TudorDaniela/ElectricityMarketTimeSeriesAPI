using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Responses;

namespace ElectricityMarketTimeSeries.Application.Services;

public interface IMarketTimeSeriesService
{
    Task<TimeSeriesResponse> GetDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> UpdateDataAsync(Guid id, TimeSeriesWriteRequest request, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> DeleteDataAsync(Guid id, CancellationToken cancellationToken);
    Task<TimeSeriesResponse> CreateDataAsync(TimeSeriesWriteRequest request, CancellationToken cancellationToken);
}
