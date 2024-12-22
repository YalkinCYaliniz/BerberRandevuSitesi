using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddFiyatlarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fiyat",
                table: "Hizmetler",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fiyat",
                table: "Hizmetler");
        }
    }
}
