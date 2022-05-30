using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TechxManagementApi.Migrations
{
    public partial class teamtask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.CreateTable(
                name: "AccountTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    AssignedToId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTasks_Accounts_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTasks_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    AssignedToId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamTasks_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamTasks_Teams_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTasks_AssignedToId",
                table: "AccountTasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTasks_CreatedById",
                table: "AccountTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTasks_ProjectId",
                table: "AccountTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTasks_AssignedToId",
                table: "TeamTasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTasks_CreatedById",
                table: "TeamTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTasks_ProjectId",
                table: "TeamTasks",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTasks");

            migrationBuilder.DropTable(
                name: "TeamTasks");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    TaskDescription = table.Column<string>(type: "text", nullable: true),
                    TaskTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");
        }
    }
}
