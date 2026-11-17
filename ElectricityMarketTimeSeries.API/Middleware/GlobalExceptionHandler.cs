using ElectricityMarketTimeSeries.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityMarketTimeSeries.API.Middleware;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService,ILogger<GlobalExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred.");

        httpContext.Response.StatusCode = ExceptionMapping.ToStatusCode(exception);

        var problem = new ProblemDetails
        {
            Title = "An error occured",
            Detail = env.IsDevelopment() ? exception.Message : "An error occurred.",
            Status = httpContext.Response.StatusCode,
            Type = exception.GetType().Name,
            Instance = httpContext.Request.Path
        };
        problem.Extensions["traceId"] = httpContext.TraceIdentifier;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problem
        });
    }
}
