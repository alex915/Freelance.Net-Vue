using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoOesia.Migrations
{
    public partial class companyfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FullTime",
                table: "Companies",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullTime",
                table: "Companies");
        }
    }
}
