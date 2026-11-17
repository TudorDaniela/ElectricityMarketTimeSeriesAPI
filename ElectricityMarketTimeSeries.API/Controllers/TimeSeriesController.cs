using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityMarketTimeSeries.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeSeriesController(IMarketTimeSeriesService timeSeriesService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTimeSeries([FromQuery] TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        var result = await timeSeriesService.GetDataAsync(request, cancellationToken);
        return Ok(result.TimeSeries);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTimeSeriesById(Guid id, CancellationToken cancellationToken)
    {
        var result = await timeSeriesService.GetByIdAsync(id, cancellationToken);
        return Ok(result.TimeSeries[0]);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTimeSeries([FromBody] TimeSeriesWriteRequest request, CancellationToken cancellationToken)
    {
        var result = await timeSeriesService.CreateDataAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetTimeSeriesById), new { id = result.TimeSeries[0].Id }, result.TimeSeries[0]);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTimeSeries(Guid id, [FromBody] TimeSeriesWriteRequest request, CancellationToken cancellationToken)
    {
        var result = await timeSeriesService.UpdateDataAsync(id, request, cancellationToken);
        return Ok(result.TimeSeries[0]);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTimeSeries(Guid id, CancellationToken cancellationToken)
    {
        await timeSeriesService.DeleteDataAsync(id, cancellationToken);
        return NoContent();
    }
}
