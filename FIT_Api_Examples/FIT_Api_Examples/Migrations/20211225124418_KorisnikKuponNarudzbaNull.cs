using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class KorisnikKuponNarudzbaNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikKupon_Narudzba_NarudzbaID",
                table: "KorisnikKupon");

            migrationBuilder.AlterColumn<int>(
                name: "NarudzbaID",
                table: "KorisnikKupon",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikKupon_Narudzba_NarudzbaID",
                table: "KorisnikKupon",
                column: "NarudzbaID",
                principalTable: "Narudzba",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikKupon_Narudzba_NarudzbaID",
                table: "KorisnikKupon");

            migrationBuilder.AlterColumn<int>(
                name: "NarudzbaID",
                table: "KorisnikKupon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikKupon_Narudzba_NarudzbaID",
                table: "KorisnikKupon",
                column: "NarudzbaID",
                principalTable: "Narudzba",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
