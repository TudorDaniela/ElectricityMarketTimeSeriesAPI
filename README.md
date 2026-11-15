# ElectricityMarketTimeSeriesAPI

## Run locally

With the .NET 10 SDK installed, run these commands from the solution directory:

```sh
dotnet restore
dotnet build
dotnet run --project ElectricityMarketTimeSeries.API
```

In Visual Studio, you can also select `ElectricityMarketTimeSeries.API` as the startup project and press **F5**.

## This is a .NET 10 Web API project that provides an interface for managing electricity market time series data. The project uses Entity Framework Core with SQLite as the database provider.
Decided to use .NET 10 since is the latest LTS version of .NET and it provides long-term support and stability for production applications. It also includes new features and improvements that can enhance the development experience and performance of the application.
### Architecure
The solution uses Clean Architecture to keep the codebase organized and maintainable and separate market rules from http and database storage.
The project is divided into the following layers: 
- **Domain Layer**: Contains the core business logic and entities.
- **Application Layer**: Houses the application services and use cases.
- **Infrastructure Layer**: Includes data access implementations and external service integrations.
- **Presentation Layer**: Contains the API controllers and DTOs.

### Domain Model
| Type | Definition |
| --- | --- |
| `BiddingZone` | A market area identified by a GUID and an EIC code. It also has a name and creation and update timestamps. |
| `TimeSeries` | A series with a GUID, type, measurement unit, required source bidding zone, optional target bidding zone, one period, and creation and update timestamps. |
| `Period` | Groups the time interval, resolution, and collection of points for a series. |
| `TimeInterval` | Defines the period boundaries through `PeriodStart` and `PeriodEnd`, both represented by `DateTimeOffset` in C#. |
| `TimeSeriesPoint` | A position within the period and a decimal quantity expressed in the series' measurement unit. |

### Database
The project uses SQLite as the database provider for simplicity and ease of setup. The connection string is defined in the `appsettings.json` file, and the `ElectricityMarketDbContext` class is used to interact with the database using Entity Framework Core.
The context uses configurations defined in the `Configurations` folder to map the domain entities to the database tables and columns. The database is created and migrated automatically when the application starts.

#### EF Core mappings

| SQLite table | Configured mapping |
| --- | --- |
| `BiddingZones` | Primary key `Id`. Both `Name` and `EicCode` are required. A unique index on `EicCode` prevents duplicate codes. |
| `TimeSeries` | Primary key `Id`. The required `SourceBiddingZoneId` and nullable `TargetBiddingZoneId` both reference `BiddingZones.Id`. EF tracks these foreign keys as shadow properties. Both relationships restrict deletion of a referenced zone. |
| `TimeSeriesPoints` | The period owns its points through `OwnsMany`. Each row contains `TimeSeriesId`, `Position`, and `Quantity`. The composite primary key `(TimeSeriesId, Position)` makes positions unique within a series. The application supplies the position. |

`Period` and `TimeInterval` are required owned objects mapped with `OwnsOne`. Their values share the `TimeSeries` row. EF converts the interval boundaries to integer UTC ticks in `PeriodStartUtcTicks` and `PeriodEndUtcTicks` so SQLite can compare instants when filtering by time range. On retrieval, the timestamps have a UTC offset of zero. [EF Core SQLite query limitations](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/limitations)

The following enums are stored as text columns on `TimeSeries`:

| Column | Supported values |
| --- | --- |
| `Type` | `Production`, `Consumption`, `CrossBorderFlow` |
| `MeasurementUnit` | `MW` (power), `MWh` (energy) |
| `Resolution` | `PT15M`, `PT30M`, `PT60M` |

#### EF Core migrations
The project uses EF Core migrations to manage database schema changes. The migrations are stored in the `Migrations` folder and can be applied using the `dotnet ef database update` command. The initial migration creates the necessary tables and relationships based on the domain model.

#### How the initial migration was generated
Using Package Manager Console, ran: 
`Add-Migration InitialCreate -Project ElectricityMarketTimeSeries.Infrastructure -StartupProject ElectricityMarketTimeSeries.API -OutputDir Migrations`
and
`Update-Database -Migration InitialCreate -Project ElectricityMarketTimeSeries.Infrastructure -StartupProject ElectricityMarketTimeSeries.API`
