using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class ModifikacijaTabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_DostavljacID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_ZaposlenikID",
                table: "Narudzba");

            migrationBuilder.AlterColumn<int>(
                name: "ZaposlenikID",
                table: "Narudzba",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StatusNarudzbeID",
                table: "Narudzba",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DostavljacID",
                table: "Narudzba",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_DostavljacID",
                table: "Narudzba",
                column: "DostavljacID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeID",
                table: "Narudzba",
                column: "StatusNarudzbeID",
                principalTable: "StatusNarudzbe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_ZaposlenikID",
                table: "Narudzba",
                column: "ZaposlenikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_DostavljacID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_ZaposlenikID",
                table: "Narudzba");

            migrationBuilder.AlterColumn<int>(
                name: "ZaposlenikID",
                table: "Narudzba",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusNarudzbeID",
                table: "Narudzba",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DostavljacID",
                table: "Narudzba",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_DostavljacID",
                table: "Narudzba",
                column: "DostavljacID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_StatusNarudzbe_StatusNarudzbeID",
                table: "Narudzba",
                column: "StatusNarudzbeID",
                principalTable: "StatusNarudzbe",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_ZaposlenikID",
                table: "Narudzba",
                column: "ZaposlenikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
