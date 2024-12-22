using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName","Age"},
                values: new object[,]
                {
                    { "admin1-id", 0, "af2c9c81-c15e-4894-9c52-e7220b398200", "B221210101@sakarya.edu.tr", true, false, null, "B221210101@SAKARYA.EDU.TR", "B221210101@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEGXjhzEVuNjYhxjg2gwBXOUBo8IC4vRdKKekew4djDBfrnNt9gu+7FVyIWSCf8yKMQ==", null, false, "3b274578-d0d9-4d06-991a-c9ec2913d03c", false, "B221210101@sakarya.edu.tr" },
                    { "admin2-id", 0, "9943436e-62f9-47da-ae68-90a82093d459", "B211210056@sakarya.edu.tr", true, false, null, "B211210056@SAKARYA.EDU.TR", "B211210056@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEJtx0fos3HOyovpLW3ZVn4SVUP/4gK/a1EQsUtqYGgm/o+hqTvVPsNgGIGZ4n8AcsQ==", null, false, "410b5c0d-268b-4cfc-ac3f-da999922953c", false, "B211210056@sakarya.edu.tr" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
