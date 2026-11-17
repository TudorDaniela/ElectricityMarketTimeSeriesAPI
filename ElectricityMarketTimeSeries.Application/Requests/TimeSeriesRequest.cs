using System.ComponentModel.DataAnnotations;

namespace ElectricityMarketTimeSeries.Application.Requests;

public sealed class TimeSeriesRequest : IValidatableObject
{
    [Required]
    public Guid? BiddingZoneId { get; init; }
    [Required]
    public DateTimeOffset? DateStart { get; init; }
    [Required]
    public DateTimeOffset? DateEnd { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BiddingZoneId == Guid.Empty)
        {
            yield return new ValidationResult(
                "Supply a non-empty bidding zone ID.",
                [nameof(BiddingZoneId)]);
        }

        if (DateStart is { } from && DateEnd is { } to && to <= from)
        {
            yield return new ValidationResult(
                "DateEnd must be later than DateStart.",
                [nameof(DateEnd)]);
        }
    }
}