using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricityMarketTimeSeries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiddingZones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    EicCode = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiddingZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    SourceBiddingZoneId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetBiddingZoneId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "TEXT", nullable: false),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    PeriodEndUtcTicks = table.Column<long>(type: "INTEGER", nullable: false),
                    PeriodStartUtcTicks = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSeries_BiddingZones_SourceBiddingZoneId",
                        column: x => x.SourceBiddingZoneId,
                        principalTable: "BiddingZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSeries_BiddingZones_TargetBiddingZoneId",
                        column: x => x.TargetBiddingZoneId,
                        principalTable: "BiddingZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSeriesPoints",
                columns: table => new
                {
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeSeriesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSeriesPoints", x => new { x.TimeSeriesId, x.Position });
                    table.ForeignKey(
                        name: "FK_TimeSeriesPoints_TimeSeries_TimeSeriesId",
                        column: x => x.TimeSeriesId,
                        principalTable: "TimeSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiddingZones_EicCode",
                table: "BiddingZones",
                column: "EicCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSeries_SourceBiddingZoneId",
                table: "TimeSeries",
                column: "SourceBiddingZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSeries_TargetBiddingZoneId",
                table: "TimeSeries",
                column: "TargetBiddingZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSeriesPoints");

            migrationBuilder.DropTable(
                name: "TimeSeries");

            migrationBuilder.DropTable(
                name: "BiddingZones");
        }
    }
}
