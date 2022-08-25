using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApi.Migrations
{
    public partial class Ladezeit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LadezeitpunktId",
                table: "WeatherMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ladezeitpunkte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uhrzeit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ladezeitpunkte", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMeasurements_LadezeitpunktId",
                table: "WeatherMeasurements",
                column: "LadezeitpunktId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherMeasurements_Ladezeitpunkte_LadezeitpunktId",
                table: "WeatherMeasurements",
                column: "LadezeitpunktId",
                principalTable: "Ladezeitpunkte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherMeasurements_Ladezeitpunkte_LadezeitpunktId",
                table: "WeatherMeasurements");

            migrationBuilder.DropTable(
                name: "Ladezeitpunkte");

            migrationBuilder.DropIndex(
                name: "IX_WeatherMeasurements_LadezeitpunktId",
                table: "WeatherMeasurements");

            migrationBuilder.DropColumn(
                name: "LadezeitpunktId",
                table: "WeatherMeasurements");
        }
    }
}
