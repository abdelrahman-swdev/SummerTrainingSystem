using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class AlterNameOfCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_HrCompanies_HrCompanyId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Students_StudentId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_StudentId",
                table: "Comments",
                newName: "IX_Comments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_HrCompanyId",
                table: "Comments",
                newName: "IX_Comments_HrCompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_HrCompanies_HrCompanyId",
                table: "Comments",
                column: "HrCompanyId",
                principalTable: "HrCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Students_StudentId",
                table: "Comments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_HrCompanies_HrCompanyId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Students_StudentId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_StudentId",
                table: "Comment",
                newName: "IX_Comment_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_HrCompanyId",
                table: "Comment",
                newName: "IX_Comment_HrCompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_HrCompanies_HrCompanyId",
                table: "Comment",
                column: "HrCompanyId",
                principalTable: "HrCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Students_StudentId",
                table: "Comment",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
