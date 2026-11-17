using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricityMarketTimeSeries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleTimeSeries : Migration
    {
        /// <inheritdoc />
        // IDs assigned by the SeedNordicBiddingZones migration.
        private const string Dk1 = "00000000-0000-4000-8000-000000000011";
        private const string Dk2 = "00000000-0000-4000-8000-000000000012";

        private static readonly (
            string Id,
            string Type,
            string SourceId,
            string? TargetId,
            decimal FirstQuantity,
            decimal SecondQuantity
        )[] Samples =
        [
            ("10000000-0000-4000-8000-000000000001", "Production",      Dk1, null, 120.5m, 125m),
    ("10000000-0000-4000-8000-000000000002", "Consumption",     Dk1, null,  95m,    98.5m),
    ("10000000-0000-4000-8000-000000000003", "CrossBorderFlow", Dk1, Dk2,   20m,    22m),
    ("10000000-0000-4000-8000-000000000004", "CrossBorderFlow", Dk2, Dk1,    5m,     4m)
        ];

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var start = new DateTimeOffset(2026, 11, 17, 10, 0, 0, TimeSpan.Zero);
            var end = start.AddMinutes(30);

            foreach (var sample in Samples)
            {
                var seriesId = Guid.Parse(sample.Id);
                Guid? targetId = sample.TargetId is null
                    ? null
                    : Guid.Parse(sample.TargetId);

                migrationBuilder.InsertData(
                    table: "TimeSeries",
                    columns:
                    [
                        "Id", "Type", "SourceBiddingZoneId", "TargetBiddingZoneId",
                "MeasurementUnit", "Resolution", "PeriodStartUtcTicks",
                "PeriodEndUtcTicks", "CreatedAt", "UpdatedAt"
                    ],
                    columnTypes:
                    [
                        "TEXT", "TEXT", "TEXT", "TEXT", "TEXT",
                "TEXT", "INTEGER", "INTEGER", "TEXT", "TEXT"
                    ],
                    values: new object?[]
                    {
                seriesId, sample.Type, Guid.Parse(sample.SourceId), targetId,
                "MW", "PT15M", start.UtcTicks, end.UtcTicks, start, start
                    });

                migrationBuilder.InsertData(
                    table: "TimeSeriesPoints",
                    columns: ["TimeSeriesId", "Position", "Quantity"],
                    columnTypes: ["TEXT", "INTEGER", "TEXT"],
                    values: new object[,]
                    {
                { seriesId, 1, sample.FirstQuantity },
                { seriesId, 2, sample.SecondQuantity }
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var sample in Samples)
            {
                var seriesId = Guid.Parse(sample.Id);

                migrationBuilder.DeleteData(
                    table: "TimeSeriesPoints",
                    keyColumn: "TimeSeriesId",
                    keyColumnType: "TEXT",
                    keyValue: seriesId);

                migrationBuilder.DeleteData(
                    table: "TimeSeries",
                    keyColumn: "Id",
                    keyColumnType: "TEXT",
                    keyValue: seriesId);
            }
        }
    }
}
