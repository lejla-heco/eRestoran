using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class KuponKorisnikNarudzba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NarudzbaID",
                table: "KorisnikKupon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikKupon_NarudzbaID",
                table: "KorisnikKupon",
                column: "NarudzbaID");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikKupon_Narudzba_NarudzbaID",
                table: "KorisnikKupon",
                column: "NarudzbaID",
                principalTable: "Narudzba",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikKupon_Narudzba_NarudzbaID",
                table: "KorisnikKupon");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikKupon_NarudzbaID",
                table: "KorisnikKupon");

            migrationBuilder.DropColumn(
                name: "NarudzbaID",
                table: "KorisnikKupon");
        }
    }
}
