using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Absalyamov_WEB2.Migrations
{
    public partial class CreateInitial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserCardRelationships",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserCardRelationships");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserCardRelationships",
                newName: "ID");
        }
    }
}
