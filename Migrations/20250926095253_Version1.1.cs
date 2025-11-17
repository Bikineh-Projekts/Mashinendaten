using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaschinenDataein.Migrations
{
    public partial class Version11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planungs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschineID = table.Column<long>(type: "bigint", nullable: false),
                    Personalsoll = table.Column<int>(type: "int", nullable: true),
                    Personalnamen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Artikel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sollmenge = table.Column<int>(type: "int", nullable: true),
                    MHD = table.Column<DateTime>(type: "date", nullable: true),
                    Kartonsanzahl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PersonalIst = table.Column<int>(type: "int", nullable: true),
                    Fertigware = table.Column<int>(type: "int", nullable: true),
                    Starten = table.Column<TimeSpan>(type: "time", nullable: true),
                    Stoppen = table.Column<TimeSpan>(type: "time", nullable: true),
                    Pause = table.Column<int>(type: "int", nullable: true),
                    Von = table.Column<TimeSpan>(type: "time", nullable: true),
                    Bis = table.Column<TimeSpan>(type: "time", nullable: true),
                    Grund = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planungs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planungs");
        }
    }
}
