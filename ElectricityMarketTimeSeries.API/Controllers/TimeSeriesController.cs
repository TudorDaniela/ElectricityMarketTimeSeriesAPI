using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityMarketTimeSeries.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeSeriesController(IMarketTimeSeriesService timeSeriesService, ILogger<TimeSeriesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTimeSeries([FromBody]TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        // Implementation for fetching time series data
        try
        {
            await timeSeriesService.GetDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching time series data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTimeSeries([FromBody]TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await timeSeriesService.CreateDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating time series data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTimeSeries([FromBody]TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await timeSeriesService.UpdateDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating time series data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTimeSeries([FromBody]TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await timeSeriesService.DeleteDataAsync(request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting time series data.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
