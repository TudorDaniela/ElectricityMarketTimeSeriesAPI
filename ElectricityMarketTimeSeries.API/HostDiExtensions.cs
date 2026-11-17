using ElectricityMarketTimeSeries.Application;
using ElectricityMarketTimeSeries.Application.Services;
using ElectricityMarketTimeSeries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ElectricityMarketTimeSeries.API;

public static class HostDiExtensions
{
    public static IServiceCollection AddWebHostInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMarketTimeSeriesService, MarketTimeSeriesService>();
        services.AddScoped<IMarketBiddingZonesService, MarketBiddingZonesService>();
        services.AddScoped<ITimeSeriesRepository, TimeSeriesRepository>();

        services.AddDbContext<ElectricityMarketDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    public static void AddHostLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
    }
}
