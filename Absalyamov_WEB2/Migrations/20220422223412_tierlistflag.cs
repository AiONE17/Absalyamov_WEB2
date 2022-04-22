using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Absalyamov_WEB2.Migrations
{
    public partial class tierlistflag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RegisteredToTierList",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredToTierList",
                table: "Users");
        }
    }
}
