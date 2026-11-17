using ElectricityMarketTimeSeries.Application.Requests;

namespace ElectricityMarketTimeSeries.Application.Services;

public interface IMarketTimeSeriesService
{
    Task GetData(TimeSeriesRequest request);
    Task UpdateData(TimeSeriesRequest request);
    Task DeleteData(TimeSeriesRequest request);
    Task CreateData(TimeSeriesRequest request);
}
