using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recepti.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recept",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Vreme = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vegan = table.Column<int>(type: "int", nullable: true),
                    Za_deca = table.Column<int>(type: "int", nullable: true),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kreator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recept", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikRecepti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceptiId = table.Column<int>(type: "int", nullable: false),
                    ReceptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikRecepti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikRecepti_Recept_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Recept",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Korisnik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocena = table.Column<int>(type: "int", nullable: true),
                    ReceptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzija_Recept_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Recept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikRecepti_ReceptId",
                table: "KorisnikRecepti",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_ReceptId",
                table: "Recenzija",
                column: "ReceptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikRecepti");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Recept");
        }
    }
}
