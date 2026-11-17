using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace ElectricityMarketTimeSeries.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BiddingZonesController(IMarketBiddingZonesService biddingZoneService, ILogger<BiddingZonesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await biddingZoneService.GetDataAsync(request, cancellationToken);
            return Ok();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching bidding zone data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromQuery]BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await biddingZoneService.CreateDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating bidding zone data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery]BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await biddingZoneService.UpdateDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating bidding zone data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery]BiddingZoneRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await biddingZoneService.DeleteDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting bidding zone data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
