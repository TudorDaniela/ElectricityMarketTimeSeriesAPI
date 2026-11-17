using ElectricityMarketTimeSeries.Domain;

namespace ElectricityMarketTimeSeries.Application.Responses;

public class TimeSeriesResponse(List<TimeSeries> data)
{
    private List<TimeSeries> _data = data;

    public IReadOnlyList<TimeSeries> TimeSeries => _data;
}
