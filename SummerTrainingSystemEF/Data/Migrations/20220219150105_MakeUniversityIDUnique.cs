using Microsoft.EntityFrameworkCore.Migrations;

namespace SummerTrainingSystemEF.Data.Migrations
{
    public partial class MakeUniversityIDUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "6c68f907-dd35-4ccc-9974-3391420be384", "2ec8eb70-198b-43a5-9296-ccd4d017fb9a", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecd5debd-0ed1-4b46-9063-80d762301216", "51b1dfce-2338-4514-8133-61df2b8b7ce9", "Supervisor", "SUPERVISOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31247f67-1c0b-4d4e-a691-1f7dd4eee69d", "57561507-0aad-4d8a-98dc-2c23d1c49285", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_UniversityID",
                table: "Supervisors",
                column: "UniversityID",
                unique: true,
                filter: "[UniversityID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UniversityID",
                table: "Students",
                column: "UniversityID",
                unique: true,
                filter: "[UniversityID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Supervisors_UniversityID",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Students_UniversityID",
                table: "Students");

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
    }
}
