using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class LinkTrainingsWithTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingTypeId",
                table: "Trainnings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trainnings_TrainingTypeId",
                table: "Trainnings",
                column: "TrainingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainnings_TrainingTypes_TrainingTypeId",
                table: "Trainnings",
                column: "TrainingTypeId",
                principalTable: "TrainingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainnings_TrainingTypes_TrainingTypeId",
                table: "Trainnings");

            migrationBuilder.DropIndex(
                name: "IX_Trainnings_TrainingTypeId",
                table: "Trainnings");

            migrationBuilder.DropColumn(
                name: "TrainingTypeId",
                table: "Trainnings");
        }
    }
}
