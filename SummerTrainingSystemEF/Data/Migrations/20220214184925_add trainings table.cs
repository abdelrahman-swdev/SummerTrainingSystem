using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystem.Data.Migrations
{
    public partial class addtrainingstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainnings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainnings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainnings_DepartmentId",
                table: "Trainnings",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainnings");
        }
    }
}
