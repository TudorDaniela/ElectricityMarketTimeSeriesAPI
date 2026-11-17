using System.ComponentModel.DataAnnotations;
using ElectricityMarketTimeSeries.Domain;

namespace ElectricityMarketTimeSeries.Application.Requests;

public sealed class TimeSeriesWriteRequest : IValidatableObject
{
    [Required]
    public TimeSeriesType? Type { get; init; }

    [Required]
    public Guid? SourceBiddingZoneId { get; init; }

    public Guid? TargetBiddingZoneId { get; init; }

    [Required]
    public MeasurementUnit? MeasurementUnit { get; init; }

    [Required]
    public Resolution? Resolution { get; init; }

    [Required]
    public DateTimeOffset? PeriodStart { get; init; }

    [Required]
    public DateTimeOffset? PeriodEnd { get; init; }

    [Required]
    public List<TimeSeriesPoint>? Points { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SourceBiddingZoneId == Guid.Empty)
        {
            yield return new ValidationResult(
                "Supply a non-empty source bidding zone ID.",
                [nameof(SourceBiddingZoneId)]);
        }

        if (TargetBiddingZoneId == Guid.Empty)
        {
            yield return new ValidationResult(
                "Target bidding zone ID must be non-empty when supplied.",
                [nameof(TargetBiddingZoneId)]);
        }

        if (PeriodStart is { } from && PeriodEnd is { } to && to <= from)
        {
            yield return new ValidationResult(
                "PeriodEnd must be later than PeriodStart.",
                [nameof(PeriodEnd)]);
        }

        if (Type is { } type && !Enum.IsDefined(type))
        {
            yield return new ValidationResult("Supply a supported time-series type.", [nameof(Type)]);
        }

        if (MeasurementUnit is { } unit && !Enum.IsDefined(unit))
        {
            yield return new ValidationResult("Supply a supported measurement unit.", [nameof(MeasurementUnit)]);
        }

        if (Resolution is { } resolution && !Enum.IsDefined(resolution))
        {
            yield return new ValidationResult("Supply a supported resolution.", [nameof(Resolution)]);
        }

        if (Points is { } points && points.Any(point => point is null))
        {
            yield return new ValidationResult("Points cannot contain null entries.", [nameof(Points)]);
        }
        else if (Points is { } validPoints
            && validPoints.Select(point => point.Position).Distinct().Count() != validPoints.Count)
        {
            yield return new ValidationResult("Point positions must be unique within a time series.", [nameof(Points)]);
        }
    }
}
