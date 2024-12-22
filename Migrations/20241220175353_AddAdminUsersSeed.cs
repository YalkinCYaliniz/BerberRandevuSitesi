using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUsersSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin1-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin2-id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin1-id", 0, "e7d8dba2-0eb6-403d-a189-712490494394", "B221210101@sakarya.edu.tr", true, false, null, "B221210101@SAKARYA.EDU.TR", "B221210101@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEH+yuqEPzg2egAKx96m4cMyVd0cha0u2KlY0ZLvudASlTv3kK2TdRnAfvipAGwIK7Q==", null, false, "563a84f4-3fe2-4a94-9c37-3c08cef4d6cb", false, "B221210101@sakarya.edu.tr" },
                    { "admin2-id", 0, "0ec2fb91-2777-43f5-b3d0-935ccd06c2a9", "B211210056@sakarya.edu.tr", true, false, null, "B211210056@SAKARYA.EDU.TR", "B211210056@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEHvLA7uzCFBqzTP+X6amPoC/ePw5qD4yM0fPSillhqL7aYasbW8FTjWsOiAyY9Qmfw==", null, false, "0152f6d2-f4d4-4d4e-838c-b32933634464", false, "B211210056@sakarya.edu.tr" }
                });
        }
    }
}
