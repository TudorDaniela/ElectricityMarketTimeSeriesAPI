using System.ComponentModel.DataAnnotations;

namespace ElectricityMarketTimeSeries.Application.Requests;

public sealed class BiddingZoneRequest : IValidatableObject
{
    [Required]
    public Guid? BiddingZoneId { get; init; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BiddingZoneId == Guid.Empty)
        {
            yield return new ValidationResult(
                "Supply a non-empty bidding zone ID.",
                [nameof(BiddingZoneId)]);
        }
    }
}
