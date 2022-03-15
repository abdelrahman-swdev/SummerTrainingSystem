using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class LinkTrainingsWithTheirCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanytId",
                table: "Trainnings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanytId",
                table: "Trainnings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
