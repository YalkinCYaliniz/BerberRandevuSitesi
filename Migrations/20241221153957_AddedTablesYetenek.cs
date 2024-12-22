using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTablesYetenek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalisanYetenekler",
                columns: table => new
                {
                    CalisanId = table.Column<int>(type: "integer", nullable: false),
                    HizmetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanYetenekler", x => new { x.CalisanId, x.HizmetId });
                    table.ForeignKey(
                        name: "FK_CalisanYetenekler_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanYetenekler_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanYetenekler_HizmetId",
                table: "CalisanYetenekler",
                column: "HizmetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanYetenekler");
        }
    }
}
