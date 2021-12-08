using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class NoveTabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Zaposlenik_GeneralUser_GeneralUserID",
                table: "Zaposlenik");

            migrationBuilder.DropTable(
                name: "Dostavljac");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "GeneralUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zaposlenik",
                table: "Zaposlenik");

            migrationBuilder.DropIndex(
                name: "IX_Zaposlenik_GeneralUserID",
                table: "Zaposlenik");

            migrationBuilder.DropColumn(
                name: "GeneralUserID",
                table: "Zaposlenik");

            migrationBuilder.RenameTable(
                name: "Zaposlenik",
                newName: "KorisnickiNalog");

            migrationBuilder.AddColumn<int>(
                name: "BrojStavki",
                table: "Narudzba",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Zakljucena",
                table: "Narudzba",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ObavljeneNarudzbe",
                table: "KorisnickiNalog",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "KorisnickiNalog",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UlogaID",
                table: "KorisnickiNalog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdresaStanovanja",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpstinaID",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DostavljeneNarudzbe",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zaposlenik_Slika",
                table: "KorisnickiNalog",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnickiNalog",
                table: "KorisnickiNalog",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vrijednost = table.Column<string>(nullable: true),
                    KorisnickiNalogId = table.Column<int>(nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(nullable: false),
                    ipAdresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_UlogaID",
                table: "KorisnickiNalog",
                column: "UlogaID");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_OpstinaID",
                table: "KorisnickiNalog",
                column: "OpstinaID");

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_KorisnickiNalogId",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Uloga_UlogaID",
                table: "KorisnickiNalog",
                column: "UlogaID",
                principalTable: "Uloga",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Opstina_OpstinaID",
                table: "KorisnickiNalog",
                column: "OpstinaID",
                principalTable: "Opstina",
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
                onDelete: ReferentialAction.NoAction);

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
                onDelete: ReferentialAction.NoAction);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Uloga_UlogaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Opstina_OpstinaID",
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
                name: "AutentifikacijaToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnickiNalog",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_KorisnickiNalog_UlogaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_KorisnickiNalog_OpstinaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "BrojStavki",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "Zakljucena",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "UlogaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "AdresaStanovanja",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "OpstinaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "DostavljeneNarudzbe",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Zaposlenik_Slika",
                table: "KorisnickiNalog");

            migrationBuilder.RenameTable(
                name: "KorisnickiNalog",
                newName: "Zaposlenik");

            migrationBuilder.AlterColumn<int>(
                name: "ObavljeneNarudzbe",
                table: "Zaposlenik",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GeneralUserID",
                table: "Zaposlenik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zaposlenik",
                table: "Zaposlenik",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "GeneralUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UlogaID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GeneralUser_Uloga_UlogaID",
                        column: x => x.UlogaID,
                        principalTable: "Uloga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Dostavljac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DostavljeneNarudzbe = table.Column<int>(type: "int", nullable: false),
                    GeneralUserID = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostavljac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dostavljac_GeneralUser_GeneralUserID",
                        column: x => x.GeneralUserID,
                        principalTable: "GeneralUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresaStanovanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralUserID = table.Column<int>(type: "int", nullable: false),
                    OpstinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnik_GeneralUser_GeneralUserID",
                        column: x => x.GeneralUserID,
                        principalTable: "GeneralUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Korisnik_Opstina_OpstinaID",
                        column: x => x.OpstinaID,
                        principalTable: "Opstina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenik_GeneralUserID",
                table: "Zaposlenik",
                column: "GeneralUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Dostavljac_GeneralUserID",
                table: "Dostavljac",
                column: "GeneralUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUser_UlogaID",
                table: "GeneralUser",
                column: "UlogaID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_GeneralUserID",
                table: "Korisnik",
                column: "GeneralUserID");

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
                onDelete: ReferentialAction.NoAction);

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
                onDelete: ReferentialAction.NoAction);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Zaposlenik_GeneralUser_GeneralUserID",
                table: "Zaposlenik",
                column: "GeneralUserID",
                principalTable: "GeneralUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
