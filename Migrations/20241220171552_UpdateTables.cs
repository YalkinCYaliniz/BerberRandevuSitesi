using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin1-id",
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 0, "af2c9c81-c15e-4894-9c52-e7220b398200", "AQAAAAIAAYagAAAAEGXjhzEVuNjYhxjg2gwBXOUBo8IC4vRdKKekew4djDBfrnNt9gu+7FVyIWSCf8yKMQ==", "3b274578-d0d9-4d06-991a-c9ec2913d03c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin2-id",
                columns: new[] { "Age", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 0, "9943436e-62f9-47da-ae68-90a82093d459", "AQAAAAIAAYagAAAAEJtx0fos3HOyovpLW3ZVn4SVUP/4gK/a1EQsUtqYGgm/o+hqTvVPsNgGIGZ4n8AcsQ==", "410b5c0d-268b-4cfc-ac3f-da999922953c" });
        }
    }
}
