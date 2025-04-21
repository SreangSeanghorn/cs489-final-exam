using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstronautSatelliteAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAstronautSatellitesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AstronautSatellite_Astronauts_AstronautsId",
                table: "AstronautSatellite");

            migrationBuilder.DropForeignKey(
                name: "FK_AstronautSatellite_Satellites_SatellitesId",
                table: "AstronautSatellite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AstronautSatellite",
                table: "AstronautSatellite");

            migrationBuilder.RenameTable(
                name: "AstronautSatellite",
                newName: "AstronautSatellites");

            migrationBuilder.RenameIndex(
                name: "IX_AstronautSatellite_SatellitesId",
                table: "AstronautSatellites",
                newName: "IX_AstronautSatellites_SatellitesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AstronautSatellites",
                table: "AstronautSatellites",
                columns: new[] { "AstronautsId", "SatellitesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AstronautSatellites_Astronauts_AstronautsId",
                table: "AstronautSatellites",
                column: "AstronautsId",
                principalTable: "Astronauts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AstronautSatellites_Satellites_SatellitesId",
                table: "AstronautSatellites",
                column: "SatellitesId",
                principalTable: "Satellites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AstronautSatellites_Astronauts_AstronautsId",
                table: "AstronautSatellites");

            migrationBuilder.DropForeignKey(
                name: "FK_AstronautSatellites_Satellites_SatellitesId",
                table: "AstronautSatellites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AstronautSatellites",
                table: "AstronautSatellites");

            migrationBuilder.RenameTable(
                name: "AstronautSatellites",
                newName: "AstronautSatellite");

            migrationBuilder.RenameIndex(
                name: "IX_AstronautSatellites_SatellitesId",
                table: "AstronautSatellite",
                newName: "IX_AstronautSatellite_SatellitesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AstronautSatellite",
                table: "AstronautSatellite",
                columns: new[] { "AstronautsId", "SatellitesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AstronautSatellite_Astronauts_AstronautsId",
                table: "AstronautSatellite",
                column: "AstronautsId",
                principalTable: "Astronauts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AstronautSatellite_Satellites_SatellitesId",
                table: "AstronautSatellite",
                column: "SatellitesId",
                principalTable: "Satellites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
