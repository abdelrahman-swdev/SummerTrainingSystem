using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class LinkTrainingsWithCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "044ae6a0-731c-4531-bddc-df073f2dd0d1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e429bd3-b0df-434a-87d3-336f8d1e91d3");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6ad0cc3f-ad47-4c62-a988-949823888779");

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Trainnings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanytId",
                table: "Trainnings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Specialities",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "HrCompanies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "HrCompanies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "HrCompanies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutCompany",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainnings_CompanyId",
                table: "Trainnings",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainnings_HrCompanies_CompanyId",
                table: "Trainnings",
                column: "CompanyId",
                principalTable: "HrCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainnings_HrCompanies_CompanyId",
                table: "Trainnings");

            migrationBuilder.DropIndex(
                name: "IX_Trainnings_CompanyId",
                table: "Trainnings");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Trainnings");

            migrationBuilder.DropColumn(
                name: "CompanytId",
                table: "Trainnings");

            migrationBuilder.AlterColumn<string>(
                name: "Specialities",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "AboutCompany",
                table: "HrCompanies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ad0cc3f-ad47-4c62-a988-949823888779", "170a4a24-bc0d-4ade-b0d9-cdbcffea28fb", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e429bd3-b0df-434a-87d3-336f8d1e91d3", "60273681-e55b-4db6-bf43-78f22c7b50f4", "Supervisor", "SUPERVISOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "044ae6a0-731c-4531-bddc-df073f2dd0d1", "010ef83c-ed94-4a0e-902f-2cde8cfcd7db", "Admin", "ADMIN" });
        }
    }
}
