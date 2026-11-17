using Microsoft.AspNetCore.Http;

namespace ElectricityMarketTimeSeries.Application.Exceptions;

public static class ExceptionMapping
{
    public static int ToStatusCode(Exception exception) => exception switch
    {
        ValidationException => StatusCodes.Status400BadRequest,
        NotFoundException => StatusCodes.Status404NotFound,
        ForbiddenException => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError
    };

    public static string ToTitle(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "Validation failed",
        StatusCodes.Status403Forbidden => "Access denied",
        StatusCodes.Status404NotFound => "Resource not found",
        _ => "An unexpected error occurred"
    };
}
