using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finops.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Resources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
