using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystem.Data.Migrations
{
    public partial class adddepartmenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Studets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studets_DepartmentId",
                table: "Studets",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studets_Department_DepartmentId",
                table: "Studets",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studets_Department_DepartmentId",
                table: "Studets");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Studets_DepartmentId",
                table: "Studets");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Studets");
        }
    }
}
