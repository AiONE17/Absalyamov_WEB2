using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Absalyamov_WEB2.Migrations
{
    public partial class CreatInitial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defending",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dribling",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pace",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Passing",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Physical",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Shooting",
                table: "UserCardRelationships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defending",
                table: "UserCardRelationships");

            migrationBuilder.DropColumn(
                name: "Dribling",
                table: "UserCardRelationships");

            migrationBuilder.DropColumn(
                name: "Pace",
                table: "UserCardRelationships");

            migrationBuilder.DropColumn(
                name: "Passing",
                table: "UserCardRelationships");

            migrationBuilder.DropColumn(
                name: "Physical",
                table: "UserCardRelationships");

            migrationBuilder.DropColumn(
                name: "Shooting",
                table: "UserCardRelationships");
        }
    }
}
