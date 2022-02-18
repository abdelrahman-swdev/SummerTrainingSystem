using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class SeedRolesWithNormalizedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "159160f9-a476-4174-b7a0-6d0b13d39b07", "507ae181-8e76-44d5-9a86-a7042a7b921b", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "426d8d44-e0c9-4321-8af2-5c2be47a86d1", "6b420049-5704-4830-a9a5-8b22c799e4d3", "Supervisor", "SUPERVISOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2337e7e-c977-4b29-abff-9a6119b254b8", "284af12f-374d-4a92-8b89-931a5aec301e", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "159160f9-a476-4174-b7a0-6d0b13d39b07");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "426d8d44-e0c9-4321-8af2-5c2be47a86d1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b2337e7e-c977-4b29-abff-9a6119b254b8");

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
    }
}
