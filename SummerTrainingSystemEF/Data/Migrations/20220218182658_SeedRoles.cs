using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43d3da47-d8f7-4f4e-9223-3da092617061", "7958d56a-8c59-47b4-8b88-6f172d91ab41", "Student", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "436f2515-102c-4a4e-8b10-07e223243f07", "095fe5cc-6caa-44f9-b32e-e73a3114983d", "Supervisor", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fd15c0c-0898-44bc-9cb9-8771d62920e4", "03626fe4-207b-4356-a43e-6b4ab38c1f74", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "436f2515-102c-4a4e-8b10-07e223243f07");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "43d3da47-d8f7-4f4e-9223-3da092617061");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5fd15c0c-0898-44bc-9cb9-8771d62920e4");
        }
    }
}
