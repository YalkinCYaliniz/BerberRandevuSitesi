using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin1-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7d8dba2-0eb6-403d-a189-712490494394", "AQAAAAIAAYagAAAAEH+yuqEPzg2egAKx96m4cMyVd0cha0u2KlY0ZLvudASlTv3kK2TdRnAfvipAGwIK7Q==", "563a84f4-3fe2-4a94-9c37-3c08cef4d6cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin2-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ec2fb91-2777-43f5-b3d0-935ccd06c2a9", "AQAAAAIAAYagAAAAEHvLA7uzCFBqzTP+X6amPoC/ePw5qD4yM0fPSillhqL7aYasbW8FTjWsOiAyY9Qmfw==", "0152f6d2-f4d4-4d4e-838c-b32933634464" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin1-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc16103c-94e0-4ace-bb82-1d64fc7c216b", "AQAAAAIAAYagAAAAEDWJKqrGZFx72lfZDWPm7zBaCcrqCZLNXE9948872+0uJcB7FZ0l361PLt2rK51JSA==", "451aea38-5279-4170-b41f-be6b83df69bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin2-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a3304de-9650-4b63-9903-7693beec2634", "AQAAAAIAAYagAAAAEHYC1003lb/iwJBGuM82QicKJmJI5bVLFP6IBQLMySFHTJaREnDSProJxVZ8u5JBlQ==", "1fe2bbf9-8822-4cdc-bbe1-d64db34eea37" });
        }
    }
}
