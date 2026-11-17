using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace ElectricityMarketTimeSeries.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BiddingZonesController(IMarketBiddingZonesService biddingZoneService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        var result = await biddingZoneService.GetDataAsync(request, cancellationToken);
        return Ok(result.BiddingZones);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] BiddingZoneRequest request, CancellationToken cancellationToken)
    {

        await biddingZoneService.CreateDataAsync(request, cancellationToken);
        return Ok();

    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        await biddingZoneService.UpdateDataAsync(request, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] BiddingZoneRequest request, CancellationToken cancellationToken)
    {

        await biddingZoneService.DeleteDataAsync(request, cancellationToken);
        return Ok();
    }
}
