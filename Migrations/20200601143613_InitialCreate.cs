using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt_Szkola.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nauczyciel",
                columns: table => new
                {
                    NauczycielID = table.Column<string>(nullable: false),
                    Imie = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    SumaGodzinTygodniowo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nauczyciel", x => x.NauczycielID);
                });

            migrationBuilder.CreateTable(
                name: "Przedmiot",
                columns: table => new
                {
                    PrzedmiotID = table.Column<string>(nullable: false),
                    Nazwa = table.Column<string>(nullable: true),
                    DlaKlasy = table.Column<string>(nullable: true),
                    LiczbaGodzinTygodniowo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmiot", x => x.PrzedmiotID);
                });

            migrationBuilder.CreateTable(
                name: "Klasa",
                columns: table => new
                {
                    KlasaID = table.Column<string>(nullable: false),
                    Sala = table.Column<string>(nullable: true),
                    NauczycielID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasa", x => x.KlasaID);
                    table.ForeignKey(
                        name: "FK_Klasa_Nauczyciel_NauczycielID",
                        column: x => x.NauczycielID,
                        principalTable: "Nauczyciel",
                        principalColumn: "NauczycielID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uprawnienie",
                columns: table => new
                {
                    UprawnienieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UprawnienieNazwa = table.Column<string>(nullable: true),
                    NauczycielID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uprawnienie", x => x.UprawnienieID);
                    table.ForeignKey(
                        name: "FK_Uprawnienie_Nauczyciel_NauczycielID",
                        column: x => x.NauczycielID,
                        principalTable: "Nauczyciel",
                        principalColumn: "NauczycielID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zajecia",
                columns: table => new
                {
                    ZajeciaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    liczbaGodzinDzien = table.Column<int>(nullable: false),
                    NauczycielID = table.Column<string>(nullable: true),
                    KlasaID = table.Column<string>(nullable: true),
                    PrzedmiotID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zajecia", x => x.ZajeciaID);
                    table.ForeignKey(
                        name: "FK_Zajecia_Klasa_KlasaID",
                        column: x => x.KlasaID,
                        principalTable: "Klasa",
                        principalColumn: "KlasaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zajecia_Nauczyciel_NauczycielID",
                        column: x => x.NauczycielID,
                        principalTable: "Nauczyciel",
                        principalColumn: "NauczycielID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zajecia_Przedmiot_PrzedmiotID",
                        column: x => x.PrzedmiotID,
                        principalTable: "Przedmiot",
                        principalColumn: "PrzedmiotID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klasa_NauczycielID",
                table: "Klasa",
                column: "NauczycielID",
                unique: true,
                filter: "[NauczycielID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Uprawnienie_NauczycielID",
                table: "Uprawnienie",
                column: "NauczycielID");

            migrationBuilder.CreateIndex(
                name: "IX_Zajecia_KlasaID",
                table: "Zajecia",
                column: "KlasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zajecia_NauczycielID",
                table: "Zajecia",
                column: "NauczycielID");

            migrationBuilder.CreateIndex(
                name: "IX_Zajecia_PrzedmiotID",
                table: "Zajecia",
                column: "PrzedmiotID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uprawnienie");

            migrationBuilder.DropTable(
                name: "Zajecia");

            migrationBuilder.DropTable(
                name: "Klasa");

            migrationBuilder.DropTable(
                name: "Przedmiot");

            migrationBuilder.DropTable(
                name: "Nauczyciel");
        }
    }
}
