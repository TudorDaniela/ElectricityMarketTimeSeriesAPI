using ElectricityMarketTimeSeries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Entity Framework services
var connectionString = builder.Configuration.GetConnectionString("ElectricityMarketDb")
                        ?? throw new InvalidOperationException("Connection string 'ElectricityMarketDb' not found.");
builder.Services.AddDbContext<ElectricityMarketDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ElectricityMarketDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
