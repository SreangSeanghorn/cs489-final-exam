using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstronautSatelliteAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Astronauts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astronauts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstronautSatellites",
                columns: table => new
                {
                    AstronautsId = table.Column<int>(type: "int", nullable: false),
                    SatellitesId = table.Column<int>(type: "int", nullable: false)
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
