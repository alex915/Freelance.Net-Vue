using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoOesia.Migrations
{
    public partial class company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegOffice",
                table: "Companies",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Companies",
                newName: "RegOffice");
        }
    }
}
