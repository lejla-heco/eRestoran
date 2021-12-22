using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class UpdateZaposlenikDostavljacTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AktivneNarudzbe",
                table: "Zaposlenik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AktivneNarudzbe",
                table: "Dostavljac",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktivneNarudzbe",
                table: "Zaposlenik");

            migrationBuilder.DropColumn(
                name: "AktivneNarudzbe",
                table: "Dostavljac");
        }
    }
}
