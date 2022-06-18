using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechxManagementApi.Migrations
{
    public partial class taskcompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "AccountTasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "AccountTasks");
        }
    }
}
