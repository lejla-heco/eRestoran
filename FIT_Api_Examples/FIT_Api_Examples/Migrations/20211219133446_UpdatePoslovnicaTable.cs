using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class UpdatePoslovnicaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RadnoVrijeme",
                table: "Poslovnica",
                newName: "RadnoVrijemeVikend");

            migrationBuilder.AddColumn<string>(
                name: "RadnoVrijemeRedovno",
                table: "Poslovnica",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RadnoVrijemeRedovno",
                table: "Poslovnica");

            migrationBuilder.RenameColumn(
                name: "RadnoVrijemeVikend",
                table: "Poslovnica",
                newName: "RadnoVrijeme");
        }
    }
}
