using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityMarketTimeSeries.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeSeriesController(IMarketTimeSeriesService timeSeriesService, ILogger<TimeSeriesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTimeSeries([FromQuery] TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        //2026-11-18T22:00:00Z
        var timeSeries = await timeSeriesService.GetDataAsync(request, cancellationToken);
        return Ok(timeSeries);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTimeSeries([FromQuery] TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        await timeSeriesService.CreateDataAsync(request, cancellationToken);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTimeSeries([FromQuery] TimeSeriesRequest request, CancellationToken cancellationToken)
    {

        await timeSeriesService.UpdateDataAsync(request, cancellationToken);
        return Ok();

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTimeSeries([FromQuery] TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        await timeSeriesService.DeleteDataAsync(request, cancellationToken);
        return Ok();
    }
}
