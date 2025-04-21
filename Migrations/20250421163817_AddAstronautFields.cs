using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstronautSatelliteAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAstronautFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Astronauts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astronauts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrbitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decommissioned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstronautSatellites",
                columns: table => new
                {
                    AstronautsId = table.Column<long>(type: "bigint", nullable: false),
                    SatellitesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronautSatellites", x => new { x.AstronautsId, x.SatellitesId });
                    table.ForeignKey(
                        name: "FK_AstronautSatellites_Astronauts_AstronautsId",
                        column: x => x.AstronautsId,
                        principalTable: "Astronauts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AstronautSatellites_Satellites_SatellitesId",
                        column: x => x.SatellitesId,
                        principalTable: "Satellites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AstronautSatellites_SatellitesId",
                table: "AstronautSatellites",
                column: "SatellitesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AstronautSatellites");

            migrationBuilder.DropTable(
                name: "Astronauts");

            migrationBuilder.DropTable(
                name: "Satellites");
        }
    }
}
