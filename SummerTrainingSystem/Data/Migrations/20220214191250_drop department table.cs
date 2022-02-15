using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystem.Data.Migrations
{
    public partial class dropdepartmenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Trainnings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Studets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainnings_Departments_DepartmentId",
                table: "Trainnings",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
