using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kupon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<string>(nullable: true),
                    Popust = table.Column<int>(nullable: false),
                    MaksimalniBrojKorisnika = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MeniGrupa",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniGrupa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Opstina",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opstina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prigoda",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prigoda", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusNarudzbe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNarudzbe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusRezervacije",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRezervacije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Uloga",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloga", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MeniStavka",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Cijena = table.Column<float>(nullable: false),
                    Slika = table.Column<string>(nullable: true),
                    Izdvojeno = table.Column<bool>(nullable: false),
                    SnizenaCijena = table.Column<float>(nullable: false),
                    Ocjena = table.Column<float>(nullable: false),
                    MeniGrupaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniStavka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeniStavka_MeniGrupa_MeniGrupaID",
                        column: x => x.MeniGrupaID,
                        principalTable: "MeniGrupa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Poslovnica",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(nullable: true),
                    BrojTelefona = table.Column<string>(nullable: true),
                    OpstinaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslovnica", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Poslovnica_Opstina_OpstinaID",
                        column: x => x.OpstinaID,
                        principalTable: "Opstina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GeneralUser",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UlogaID = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slika = table.Column<string>(nullable: true),
                    DostavljeneNarudzbe = table.Column<int>(nullable: false),
                    GeneralUserID = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresaStanovanja = table.Column<string>(nullable: true),
                    BrojTelefona = table.Column<string>(nullable: true),
                    GeneralUserID = table.Column<int>(nullable: false),
                    OpstinaID = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Zaposlenik",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slika = table.Column<string>(nullable: true),
                    ObavljeneNarudzbe = table.Column<int>(nullable: false),
                    GeneralUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zaposlenik_GeneralUser_GeneralUserID",
                        column: x => x.GeneralUserID,
                        principalTable: "GeneralUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikKupon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iskoristen = table.Column<bool>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    KuponID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikKupon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KorisnikKupon_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KorisnikKupon_Kupon_KuponID",
                        column: x => x.KuponID,
                        principalTable: "Kupon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OmiljenaStavka",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(nullable: false),
                    MeniStavkaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OmiljenaStavka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OmiljenaStavka_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OmiljenaStavka_MeniStavka_MeniStavkaID",
                        column: x => x.MeniStavkaID,
                        principalTable: "MeniStavka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumRezerviranja = table.Column<DateTime>(nullable: false),
                    BrojOsoba = table.Column<int>(nullable: false),
                    BrojStolova = table.Column<int>(nullable: false),
                    Obavljena = table.Column<bool>(nullable: false),
                    Poruka = table.Column<string>(nullable: true),
                    PrigodaID = table.Column<int>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    StatusRezervacijeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Prigoda_PrigodaID",
                        column: x => x.PrigodaID,
                        principalTable: "Prigoda",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Rezervacija_StatusRezervacije_StatusRezervacijeID",
                        column: x => x.StatusRezervacijeID,
                        principalTable: "StatusRezervacije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omiljeno = table.Column<bool>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    DatumNarucivanja = table.Column<DateTime>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    StatusNarudzbeID = table.Column<int>(nullable: false),
                    ZaposlenikID = table.Column<int>(nullable: false),
                    DostavljacID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Narudzba_Dostavljac_DostavljacID",
                        column: x => x.DostavljacID,
                        principalTable: "Dostavljac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Narudzba_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeID",
                        column: x => x.StatusNarudzbeID,
                        principalTable: "StatusNarudzbe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Narudzba_Zaposlenik_ZaposlenikID",
                        column: x => x.ZaposlenikID,
                        principalTable: "Zaposlenik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StavkaNarudzbe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(nullable: false),
                    Iznos = table.Column<float>(nullable: false),
                    MeniStavkaID = table.Column<int>(nullable: false),
                    NarudzbaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaNarudzbe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StavkaNarudzbe_MeniStavka_MeniStavkaID",
                        column: x => x.MeniStavkaID,
                        principalTable: "MeniStavka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StavkaNarudzbe_Narudzba_NarudzbaID",
                        column: x => x.NarudzbaID,
                        principalTable: "Narudzba",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikKupon_KorisnikID",
                table: "KorisnikKupon",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikKupon_KuponID",
                table: "KorisnikKupon",
                column: "KuponID");

            migrationBuilder.CreateIndex(
                name: "IX_MeniStavka_MeniGrupaID",
                table: "MeniStavka",
                column: "MeniGrupaID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_DostavljacID",
                table: "Narudzba",
                column: "DostavljacID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_StatusNarudzbeID",
                table: "Narudzba",
                column: "StatusNarudzbeID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_ZaposlenikID",
                table: "Narudzba",
                column: "ZaposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_OmiljenaStavka_KorisnikID",
                table: "OmiljenaStavka",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_OmiljenaStavka_MeniStavkaID",
                table: "OmiljenaStavka",
                column: "MeniStavkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovnica_OpstinaID",
                table: "Poslovnica",
                column: "OpstinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KorisnikID",
                table: "Rezervacija",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_PrigodaID",
                table: "Rezervacija",
                column: "PrigodaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_StatusRezervacijeID",
                table: "Rezervacija",
                column: "StatusRezervacijeID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbe_MeniStavkaID",
                table: "StavkaNarudzbe",
                column: "MeniStavkaID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbe_NarudzbaID",
                table: "StavkaNarudzbe",
                column: "NarudzbaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenik_GeneralUserID",
                table: "Zaposlenik",
                column: "GeneralUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikKupon");

            migrationBuilder.DropTable(
                name: "OmiljenaStavka");

            migrationBuilder.DropTable(
                name: "Poslovnica");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "StavkaNarudzbe");

            migrationBuilder.DropTable(
                name: "Kupon");

            migrationBuilder.DropTable(
                name: "Prigoda");

            migrationBuilder.DropTable(
                name: "StatusRezervacije");

            migrationBuilder.DropTable(
                name: "MeniStavka");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "MeniGrupa");

            migrationBuilder.DropTable(
                name: "Dostavljac");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "StatusNarudzbe");

            migrationBuilder.DropTable(
                name: "Zaposlenik");

            migrationBuilder.DropTable(
                name: "Opstina");

            migrationBuilder.DropTable(
                name: "GeneralUser");

            migrationBuilder.DropTable(
                name: "Uloga");
        }
    }
}
