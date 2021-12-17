using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class Autentifikacija_Autorizacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Opstina_OpstinaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Uloga_UlogaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikKupon_KorisnickiNalog_KorisnikID",
                table: "KorisnikKupon");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_DostavljacID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_KorisnikID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_ZaposlenikID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_OmiljenaStavka_KorisnickiNalog_KorisnikID",
                table: "OmiljenaStavka");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_KorisnickiNalog_KorisnikID",
                table: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Uloga");

            migrationBuilder.DropIndex(
                name: "IX_KorisnickiNalog_OpstinaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_KorisnickiNalog_UlogaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "AdresaStanovanja",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "DatumKreiranja",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "DostavljeneNarudzbe",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "ObavljeneNarudzbe",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "OpstinaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Slika",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "UlogaID",
                table: "KorisnickiNalog");

            migrationBuilder.RenameColumn(
                name: "Zaposlenik_Slika",
                table: "KorisnickiNalog",
                newName: "Lozinka");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "KorisnickiNalog",
                newName: "KorisnickoIme");

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Administrator_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dostavljac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DostavljeneNarudzbe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostavljac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dostavljac_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    AdresaStanovanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpstinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnik_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Korisnik_Opstina_OpstinaID",
                        column: x => x.OpstinaID,
                        principalTable: "Opstina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObavljeneNarudzbe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zaposlenik_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_OpstinaID",
                table: "Korisnik",
                column: "OpstinaID");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikKupon_Korisnik_KorisnikID",
                table: "KorisnikKupon",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Dostavljac_DostavljacID",
                table: "Narudzba",
                column: "DostavljacID",
                principalTable: "Dostavljac",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Zaposlenik_ZaposlenikID",
                table: "Narudzba",
                column: "ZaposlenikID",
                principalTable: "Zaposlenik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OmiljenaStavka_Korisnik_KorisnikID",
                table: "OmiljenaStavka",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikID",
                table: "Rezervacija",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikKupon_Korisnik_KorisnikID",
                table: "KorisnikKupon");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Dostavljac_DostavljacID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Zaposlenik_ZaposlenikID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_OmiljenaStavka_Korisnik_KorisnikID",
                table: "OmiljenaStavka");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikID",
                table: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Dostavljac");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Zaposlenik");

            migrationBuilder.RenameColumn(
                name: "Lozinka",
                table: "KorisnickiNalog",
                newName: "Zaposlenik_Slika");

            migrationBuilder.RenameColumn(
                name: "KorisnickoIme",
                table: "KorisnickiNalog",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "AdresaStanovanja",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKreiranja",
                table: "KorisnickiNalog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DostavljeneNarudzbe",
                table: "KorisnickiNalog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObavljeneNarudzbe",
                table: "KorisnickiNalog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpstinaID",
                table: "KorisnickiNalog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UlogaID",
                table: "KorisnickiNalog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Uloga",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloga", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_OpstinaID",
                table: "KorisnickiNalog",
                column: "OpstinaID");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_UlogaID",
                table: "KorisnickiNalog",
                column: "UlogaID");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Opstina_OpstinaID",
                table: "KorisnickiNalog",
                column: "OpstinaID",
                principalTable: "Opstina",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Uloga_UlogaID",
                table: "KorisnickiNalog",
                column: "UlogaID",
                principalTable: "Uloga",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikKupon_KorisnickiNalog_KorisnikID",
                table: "KorisnikKupon",
                column: "KorisnikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_DostavljacID",
                table: "Narudzba",
                column: "DostavljacID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_ZaposlenikID",
                table: "Narudzba",
                column: "ZaposlenikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OmiljenaStavka_KorisnickiNalog_KorisnikID",
                table: "OmiljenaStavka",
                column: "KorisnikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_KorisnickiNalog_KorisnikID",
                table: "Rezervacija",
                column: "KorisnikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
