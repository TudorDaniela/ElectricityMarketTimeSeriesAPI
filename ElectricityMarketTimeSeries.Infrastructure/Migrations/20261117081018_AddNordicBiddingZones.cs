using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricityMarketTimeSeries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNordicBiddingZones : Migration
    {
        /// <inheritdoc />
        private static readonly (string Id, string Name, string EicCode)[] Zones =
        [

            ("00000000-0000-4000-8000-000000000001", "NO1", "10YNO-1--------2"),
            ("00000000-0000-4000-8000-000000000002", "NO2", "10YNO-2--------T"),
            ("00000000-0000-4000-8000-000000000003", "NO3", "10YNO-3--------J"),
            ("00000000-0000-4000-8000-000000000004", "NO4", "10YNO-4--------9"),
            ("00000000-0000-4000-8000-000000000005", "NO5", "10Y1001A1001A48H"),
            ("00000000-0000-4000-8000-000000000006", "SE1", "10Y1001A1001A44P"),
            ("00000000-0000-4000-8000-000000000007", "SE2", "10Y1001A1001A45N"),
            ("00000000-0000-4000-8000-000000000008", "SE3", "10Y1001A1001A46L"),
            ("00000000-0000-4000-8000-000000000009", "SE4", "10Y1001A1001A47J"),
            ("00000000-0000-4000-8000-000000000010", "FI",  "10YFI-1--------U"),
            ("00000000-0000-4000-8000-000000000011", "DK1", "10YDK-1--------W"),
            ("00000000-0000-4000-8000-000000000012", "DK2", "10YDK-2--------M")
];

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var seededAt = new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero);

            foreach (var zone in Zones)
            {
                migrationBuilder.InsertData(
                    table: "BiddingZones",
                    columns: ["Id", "Name", "EicCode", "CreatedAt", "UpdatedAt"],
                    values: new object[]
                    {
                Guid.Parse(zone.Id),
                zone.Name,
                zone.EicCode,
                seededAt,
                seededAt
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var zone in Zones)
            {
                migrationBuilder.DeleteData(
                    table: "BiddingZones",
                    keyColumn: "Id",
                    keyValue: Guid.Parse(zone.Id));
            }
        }
    }
}
