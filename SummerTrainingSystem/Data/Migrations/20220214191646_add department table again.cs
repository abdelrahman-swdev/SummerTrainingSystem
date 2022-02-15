using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystem.Data.Migrations
{
    public partial class adddepartmenttableagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Trainnings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Studets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainnings_DepartmentId",
                table: "Trainnings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Studets_DepartmentId",
                table: "Studets",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studets_Departments_DepartmentId",
                table: "Studets",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainnings_Departments_DepartmentId",
                table: "Trainnings",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studets_Departments_DepartmentId",
                table: "Studets");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainnings_Departments_DepartmentId",
                table: "Trainnings");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Trainnings_DepartmentId",
                table: "Trainnings");

            migrationBuilder.DropIndex(
                name: "IX_Studets_DepartmentId",
                table: "Studets");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Trainnings");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Studets");
        }
    }
}
