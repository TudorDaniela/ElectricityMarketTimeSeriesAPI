namespace ElectricityMarketTimeSeries.Domain;

public class BiddingZone
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string EicCode { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
    //NO1,
    //NO2,
    //NO3,
    //NO4,
    //NO5,
    //SE1,
    //SE2,
    //SE3,
    //SE4,
    //FI,
    //DK1,
    //DK2
}
