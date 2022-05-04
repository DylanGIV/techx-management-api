using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechxManagementApi.Migrations
{
    public partial class titletocompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Accounts",
                newName: "Company");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Accounts",
                newName: "Title");
        }
    }
}
