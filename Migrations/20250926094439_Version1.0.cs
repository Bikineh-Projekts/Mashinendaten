using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaschinenDataein.Migrations
{
    public partial class Version10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maschinen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAdresse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maschinen", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Stoerungsmeldung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meldung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoerungsmeldung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zustandsmeldung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meldung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zustandsmeldung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abzugsdaten",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    PRnummer = table.Column<int>(type: "int", nullable: false),
                    PackungenproAbzug = table.Column<long>(type: "bigint", nullable: false),
                    Abzuglaenge = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abzugsdaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abzugsdaten_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarmdaten",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    AM1 = table.Column<bool>(type: "bit", nullable: false),
                    AM2 = table.Column<bool>(type: "bit", nullable: false),
                    AM3 = table.Column<bool>(type: "bit", nullable: false),
                    AM4 = table.Column<bool>(type: "bit", nullable: false),
                    AM5 = table.Column<bool>(type: "bit", nullable: false),
                    AM6 = table.Column<bool>(type: "bit", nullable: false),
                    AM7 = table.Column<bool>(type: "bit", nullable: false),
                    AM8 = table.Column<bool>(type: "bit", nullable: false),
                    AM9 = table.Column<bool>(type: "bit", nullable: false),
                    AM10 = table.Column<bool>(type: "bit", nullable: false),
                    AM11 = table.Column<bool>(type: "bit", nullable: false),
                    AM12 = table.Column<bool>(type: "bit", nullable: false),
                    AM13 = table.Column<bool>(type: "bit", nullable: false),
                    AM14 = table.Column<bool>(type: "bit", nullable: false),
                    AM15 = table.Column<bool>(type: "bit", nullable: false),
                    AM16 = table.Column<bool>(type: "bit", nullable: false),
                    AM17 = table.Column<bool>(type: "bit", nullable: false),
                    AM18 = table.Column<bool>(type: "bit", nullable: false),
                    AM19 = table.Column<bool>(type: "bit", nullable: false),
                    AM20 = table.Column<bool>(type: "bit", nullable: false),
                    AM21 = table.Column<bool>(type: "bit", nullable: false),
                    AM22 = table.Column<bool>(type: "bit", nullable: false),
                    AM23 = table.Column<bool>(type: "bit", nullable: false),
                    AM24 = table.Column<bool>(type: "bit", nullable: false),
                    AM25 = table.Column<bool>(type: "bit", nullable: false),
                    AM26 = table.Column<bool>(type: "bit", nullable: false),
                    AM27 = table.Column<bool>(type: "bit", nullable: false),
                    AM28 = table.Column<bool>(type: "bit", nullable: false),
                    AM29 = table.Column<bool>(type: "bit", nullable: false),
                    AM30 = table.Column<bool>(type: "bit", nullable: false),
                    AM31 = table.Column<bool>(type: "bit", nullable: false),
                    AM32 = table.Column<bool>(type: "bit", nullable: false),
                    AM33 = table.Column<bool>(type: "bit", nullable: false),
                    AM34 = table.Column<bool>(type: "bit", nullable: false),
                    AM35 = table.Column<bool>(type: "bit", nullable: false),
                    AM36 = table.Column<bool>(type: "bit", nullable: false),
                    AM37 = table.Column<bool>(type: "bit", nullable: false),
                    AM38 = table.Column<bool>(type: "bit", nullable: false),
                    AM39 = table.Column<bool>(type: "bit", nullable: false),
                    AM40 = table.Column<bool>(type: "bit", nullable: false),
                    AM41 = table.Column<bool>(type: "bit", nullable: false),
                    AM42 = table.Column<bool>(type: "bit", nullable: false),
                    AM43 = table.Column<bool>(type: "bit", nullable: false),
                    AM44 = table.Column<bool>(type: "bit", nullable: false),
                    AM45 = table.Column<bool>(type: "bit", nullable: false),
                    AM46 = table.Column<bool>(type: "bit", nullable: false),
                    AM47 = table.Column<bool>(type: "bit", nullable: false),
                    AM48 = table.Column<bool>(type: "bit", nullable: false),
                    AM49 = table.Column<bool>(type: "bit", nullable: false),
                    AM50 = table.Column<bool>(type: "bit", nullable: false),
                    AM51 = table.Column<bool>(type: "bit", nullable: false),
                    AM52 = table.Column<bool>(type: "bit", nullable: false),
                    AM53 = table.Column<bool>(type: "bit", nullable: false),
                    AM54 = table.Column<bool>(type: "bit", nullable: false),
                    AM55 = table.Column<bool>(type: "bit", nullable: false),
                    AM56 = table.Column<bool>(type: "bit", nullable: false),
                    AM57 = table.Column<bool>(type: "bit", nullable: false),
                    AM58 = table.Column<bool>(type: "bit", nullable: false),
                    AM59 = table.Column<bool>(type: "bit", nullable: false),
                    AM60 = table.Column<bool>(type: "bit", nullable: false),
                    AM61 = table.Column<bool>(type: "bit", nullable: false),
                    AM62 = table.Column<bool>(type: "bit", nullable: false),
                    AM63 = table.Column<bool>(type: "bit", nullable: false),
                    AM64 = table.Column<bool>(type: "bit", nullable: false),
                    AM65 = table.Column<bool>(type: "bit", nullable: false),
                    AM66 = table.Column<bool>(type: "bit", nullable: false),
                    AM67 = table.Column<bool>(type: "bit", nullable: false),
                    AM68 = table.Column<bool>(type: "bit", nullable: false),
                    AM69 = table.Column<bool>(type: "bit", nullable: false),
                    AM70 = table.Column<bool>(type: "bit", nullable: false),
                    AM71 = table.Column<bool>(type: "bit", nullable: false),
                    AM72 = table.Column<bool>(type: "bit", nullable: false),
                    AM73 = table.Column<bool>(type: "bit", nullable: false),
                    AM74 = table.Column<bool>(type: "bit", nullable: false),
                    AM75 = table.Column<bool>(type: "bit", nullable: false),
                    AM76 = table.Column<bool>(type: "bit", nullable: false),
                    AM77 = table.Column<bool>(type: "bit", nullable: false),
                    AM78 = table.Column<bool>(type: "bit", nullable: false),
                    AM79 = table.Column<bool>(type: "bit", nullable: false),
                    AM80 = table.Column<bool>(type: "bit", nullable: false),
                    AM81 = table.Column<bool>(type: "bit", nullable: false),
                    AM82 = table.Column<bool>(type: "bit", nullable: false),
                    AM83 = table.Column<bool>(type: "bit", nullable: false),
                    AM84 = table.Column<bool>(type: "bit", nullable: false),
                    AM85 = table.Column<bool>(type: "bit", nullable: false),
                    AM86 = table.Column<bool>(type: "bit", nullable: false),
                    AM87 = table.Column<bool>(type: "bit", nullable: false),
                    AM88 = table.Column<bool>(type: "bit", nullable: false),
                    AM89 = table.Column<bool>(type: "bit", nullable: false),
                    AM90 = table.Column<bool>(type: "bit", nullable: false),
                    AM91 = table.Column<bool>(type: "bit", nullable: false),
                    AM92 = table.Column<bool>(type: "bit", nullable: false),
                    AM93 = table.Column<bool>(type: "bit", nullable: false),
                    AM94 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarmdaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarmdaten_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leistungsdaten",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    PRnummer = table.Column<int>(type: "int", nullable: false),
                    Tagestaktzaehler = table.Column<int>(type: "int", nullable: false),
                    Packungszaeler = table.Column<int>(type: "int", nullable: false),
                    Maschinentakte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leistungsdaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leistungsdaten_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaschinenProgrammen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    PRnummer = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaschinenProgrammen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaschinenProgrammen_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperaturdaten",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    PRnummer = table.Column<int>(type: "int", nullable: false),
                    Solltemp1 = table.Column<int>(type: "int", nullable: false),
                    Isstemp1 = table.Column<int>(type: "int", nullable: false),
                    Solltemp2 = table.Column<int>(type: "int", nullable: false),
                    Isstemp2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperaturdaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperaturdaten_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stoerungsdaten",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    StoerungsmeldungId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoerungsdaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stoerungsdaten_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stoerungsdaten_Stoerungsmeldung_StoerungsmeldungId",
                        column: x => x.StoerungsmeldungId,
                        principalTable: "Stoerungsmeldung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zustandsdaten",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaschinenId = table.Column<long>(type: "bigint", nullable: false),
                    ZustandsmeldungId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zustandsdaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zustandsdaten_Maschinen_MaschinenId",
                        column: x => x.MaschinenId,
                        principalTable: "Maschinen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zustandsdaten_Zustandsmeldung_ZustandsmeldungId",
                        column: x => x.ZustandsmeldungId,
                        principalTable: "Zustandsmeldung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abzugsdaten_MaschinenId",
                table: "Abzugsdaten",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmdaten_MaschinenId",
                table: "Alarmdaten",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Leistungsdaten_MaschinenId",
                table: "Leistungsdaten",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_MaschinenProgrammen_MaschinenId",
                table: "MaschinenProgrammen",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoerungsdaten_MaschinenId",
                table: "Stoerungsdaten",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoerungsdaten_StoerungsmeldungId",
                table: "Stoerungsdaten",
                column: "StoerungsmeldungId");

            migrationBuilder.CreateIndex(
                name: "IX_Temperaturdaten_MaschinenId",
                table: "Temperaturdaten",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Zustandsdaten_MaschinenId",
                table: "Zustandsdaten",
                column: "MaschinenId");

            migrationBuilder.CreateIndex(
                name: "IX_Zustandsdaten_ZustandsmeldungId",
                table: "Zustandsdaten",
                column: "ZustandsmeldungId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abzugsdaten");

            migrationBuilder.DropTable(
                name: "Alarmdaten");

            migrationBuilder.DropTable(
                name: "Leistungsdaten");

            migrationBuilder.DropTable(
                name: "MaschinenProgrammen");

            migrationBuilder.DropTable(
                name: "Planungs");

            migrationBuilder.DropTable(
                name: "Stoerungsdaten");

            migrationBuilder.DropTable(
                name: "Temperaturdaten");

            migrationBuilder.DropTable(
                name: "Zustandsdaten");

            migrationBuilder.DropTable(
                name: "Stoerungsmeldung");

            migrationBuilder.DropTable(
                name: "Maschinen");

            migrationBuilder.DropTable(
                name: "Zustandsmeldung");
        }
    }
}
