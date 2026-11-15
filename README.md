# ElectricityMarketTimeSeriesAPI

## This is a .NET 10 Web API project that provides an interface for managing electricity market time series data. The project uses Entity Framework Core with SQLite as the database provider.
Decided to use .NET 10 since is the latest LTS version of .NET and it provides long-term support and stability for production applications. It also includes new features and improvements that can enhance the development experience and performance of the application.
### Architecure
The solution uses Clean Architecture to keep the codebase organized and maintainable. The project is divided into the following layers: 
- **Domain Layer**: Contains the core business logic and entities.
- **Application Layer**: Houses the application services and use cases.
- **Infrastructure Layer**: Includes data access implementations and external service integrations.
- **Presentation Layer**: Contains the API controllers and DTOs.

### Database
The project uses SQLite as the database provider for simplicity and ease of setup. The connection string is defined in the `appsettings.json` file, and the `ElectricityMarketDbContext` class is used to interact with the database using Entity Framework Core.