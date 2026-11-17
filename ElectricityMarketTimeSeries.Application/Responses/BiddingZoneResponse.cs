using ElectricityMarketTimeSeries.Domain;

namespace ElectricityMarketTimeSeries.Application.Responses;

public sealed class BiddingZoneResponse(List<BiddingZone> data)
{
    private List<BiddingZone> _data = data;
    public IReadOnlyList<BiddingZone> BiddingZones => _data;
}
