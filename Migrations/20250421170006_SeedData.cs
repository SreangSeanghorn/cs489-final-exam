using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AstronautSatelliteAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Astronauts",
                columns: new[] { "Id", "ExperienceYears", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1L, 12, "Neil", "Armstrong" },
                    { 2L, 8, "Sally", "Ride" },
                    { 3L, 15, "Chris", "Hadfield" }
                });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Decommissioned", "LaunchDate", "Name", "OrbitType" },
                values: new object[,]
                {
                    { 1L, false, new DateTime(1990, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hubble", "LEO" },
                    { 2L, false, new DateTime(2023, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Starlink-17", "MEO" },
                    { 3L, true, new DateTime(2020, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sentinel-6", "LEO" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Astronauts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Astronauts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Astronauts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 3L);

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
    }
}
