using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pozisyon",
                table: "Calisanlar");

            migrationBuilder.AddColumn<int>(
                name: "Maas",
                table: "Calisanlar",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Maas",
                table: "Calisanlar");

            migrationBuilder.AddColumn<string>(
                name: "Pozisyon",
                table: "Calisanlar",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
