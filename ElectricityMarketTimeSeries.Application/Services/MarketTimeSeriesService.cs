using ElectricityMarketTimeSeries.Application.Exceptions;
using ElectricityMarketTimeSeries.Application.Requests;
using ElectricityMarketTimeSeries.Application.Responses;
using ElectricityMarketTimeSeries.Domain;

namespace ElectricityMarketTimeSeries.Application.Services;

public class MarketTimeSeriesService(ITimeSeriesRepository repository) : IMarketTimeSeriesService
{
    public async Task<TimeSeriesResponse> GetDataAsync(TimeSeriesRequest request, CancellationToken cancellationToken)
    {
        var biddingZoneId = request.BiddingZoneId ?? throw new ValidationException("BiddingZoneId is required.");
        var dateStart = request.DateStart ?? throw new ValidationException("DateStart is required.");
        var dateEnd = request.DateEnd ?? throw new ValidationException("DateEnd is required.");
        if (biddingZoneId == Guid.Empty || dateEnd <= dateStart)
        {
            throw new ValidationException("Supply a non-empty bidding zone ID and a valid date range.");
        }

        var result = await repository.FindAsync(biddingZoneId, dateStart, dateEnd, cancellationToken);
        return new TimeSeriesResponse([.. result]);
    }

    public async Task<TimeSeriesResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var result = await repository.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Time series '{id}' was not found.");

        return new TimeSeriesResponse([result]);
    }

    public async Task<TimeSeriesResponse> UpdateDataAsync(
        Guid id,
        TimeSeriesWriteRequest request,
        CancellationToken cancellationToken)
    {
        ValidateId(id);
        ValidateWriteRequest(request);
        var sourceZoneId = request.SourceBiddingZoneId
            ?? throw new ValidationException("SourceBiddingZoneId is required.");
        var sourceZone = await repository.FindBiddingZoneAsync(sourceZoneId, cancellationToken)
            ?? throw new NotFoundException($"Source bidding zone '{sourceZoneId}' was not found.");
        var targetZone = await GetTargetZoneAsync(request.TargetBiddingZoneId, cancellationToken);

        var updatedTimeSeries = new TimeSeries
        {
            Id = id,
            Type = request.Type ?? throw new ValidationException("Type is required."),
            SourceBiddingZoneId = sourceZoneId,
            SourceBiddingZone = sourceZone,
            TargetBiddingZoneId = targetZone?.Id,
            TargetBiddingZone = targetZone,
            MeasurementUnit = request.MeasurementUnit ?? throw new ValidationException("MeasurementUnit is required."),
            Period = CreatePeriod(request),
            CreatedAt = default,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var result = await repository.UpdateAsync(updatedTimeSeries, cancellationToken)
            ?? throw new NotFoundException($"Time series '{id}' was not found.");

        return new TimeSeriesResponse([result]);
    }

    public async Task<TimeSeriesResponse> DeleteDataAsync(Guid id, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var result = await repository.DeleteAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Time series '{id}' was not found.");

        return new TimeSeriesResponse([result]);
    }

    public async Task<TimeSeriesResponse> CreateDataAsync(
        TimeSeriesWriteRequest request,
        CancellationToken cancellationToken)
    {
        ValidateWriteRequest(request);
        var sourceZoneId = request.SourceBiddingZoneId
            ?? throw new ValidationException("SourceBiddingZoneId is required.");
        var sourceZone = await repository.FindBiddingZoneAsync(sourceZoneId, cancellationToken)
            ?? throw new NotFoundException($"Source bidding zone '{sourceZoneId}' was not found.");
        var targetZone = await GetTargetZoneAsync(request.TargetBiddingZoneId, cancellationToken);
        var now = DateTimeOffset.UtcNow;

        var timeSeries = new TimeSeries
        {
            Id = Guid.NewGuid(),
            Type = request.Type ?? throw new ValidationException("Type is required."),
            SourceBiddingZoneId = sourceZoneId,
            SourceBiddingZone = sourceZone,
            TargetBiddingZoneId = targetZone?.Id,
            TargetBiddingZone = targetZone,
            MeasurementUnit = request.MeasurementUnit ?? throw new ValidationException("MeasurementUnit is required."),
            Period = CreatePeriod(request),
            CreatedAt = now,
            UpdatedAt = now
        };

        var result = await repository.CreateAsync(timeSeries, cancellationToken);
        return new TimeSeriesResponse([result]);
    }

    private async Task<BiddingZone?> GetTargetZoneAsync(Guid? targetZoneId, CancellationToken cancellationToken)
    {
        if (targetZoneId is not { } id)
        {
            return null;
        }

        return await repository.FindBiddingZoneAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Target bidding zone '{id}' was not found.");
    }

    private static void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ValidationException("Supply a non-empty time-series ID.");
        }
    }

    private static void ValidateWriteRequest(TimeSeriesWriteRequest request)
    {
        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
            request,
            new System.ComponentModel.DataAnnotations.ValidationContext(request),
            validationResults,
            validateAllProperties: true))
        {
            throw new ValidationException(string.Join(" ", validationResults.Select(result => result.ErrorMessage)));
        }
    }

    private static Period CreatePeriod(TimeSeriesWriteRequest request)
    {
        return new Period
        {
            Resolution = request.Resolution ?? throw new ValidationException("Resolution is required."),
            TimeInterval = new TimeInterval
            {
                PeriodStart = request.PeriodStart ?? throw new ValidationException("PeriodStart is required."),
                PeriodEnd = request.PeriodEnd ?? throw new ValidationException("PeriodEnd is required.")
            },
            Points = request.Points?.ToList() ?? throw new ValidationException("Points are required.")
        };
    }
}
