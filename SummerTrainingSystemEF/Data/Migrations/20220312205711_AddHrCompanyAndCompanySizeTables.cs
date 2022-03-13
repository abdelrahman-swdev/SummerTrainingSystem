using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class AddHrCompanyAndCompanySizeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "31247f67-1c0b-4d4e-a691-1f7dd4eee69d");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6c68f907-dd35-4ccc-9974-3391420be384");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ecd5debd-0ed1-4b46-9063-80d762301216");

            migrationBuilder.CreateTable(
                name: "CompanySizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SizeRange = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrCompanies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoundedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanySizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrCompanies_CompanySizes_CompanySizeId",
                        column: x => x.CompanySizeId,
                        principalTable: "CompanySizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HrCompanies_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_HrCompanies_CompanySizeId",
                table: "HrCompanies",
                column: "CompanySizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HrCompanies");

            migrationBuilder.DropTable(
                name: "CompanySizes");

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c68f907-dd35-4ccc-9974-3391420be384", "2ec8eb70-198b-43a5-9296-ccd4d017fb9a", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecd5debd-0ed1-4b46-9063-80d762301216", "51b1dfce-2338-4514-8133-61df2b8b7ce9", "Supervisor", "SUPERVISOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31247f67-1c0b-4d4e-a691-1f7dd4eee69d", "57561507-0aad-4d8a-98dc-2c23d1c49285", "Admin", "ADMIN" });
        }
    }
}
