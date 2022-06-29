using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechxManagementApi.Migrations
{
    public partial class projectstatusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Accounts",
                type: "text",
                nullable: true);
        }
    }
}
