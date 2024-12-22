using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTablesAdSoyad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ad",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "soyad",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "soyad",
                table: "AspNetUsers");
        }
    }
}
