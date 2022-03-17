using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class DeleteTrainingsWhenCompanyIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainnings_HrCompanies_CompanyId",
                table: "Trainnings");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainnings_HrCompanies_CompanyId",
                table: "Trainnings",
                column: "CompanyId",
                principalTable: "HrCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainnings_HrCompanies_CompanyId",
                table: "Trainnings");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainnings_HrCompanies_CompanyId",
                table: "Trainnings",
                column: "CompanyId",
                principalTable: "HrCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
