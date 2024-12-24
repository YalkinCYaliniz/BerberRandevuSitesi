using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberRandevuSitesi.Migrations
{
    /// <inheritdoc />
    public partial class Randevutabloo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Tarih",
                table: "Randevular",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Randevular",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_ApplicationUserId",
                table: "Randevular",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_ApplicationUserId",
                table: "Randevular",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_ApplicationUserId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_ApplicationUserId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Randevular");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Tarih",
                table: "Randevular",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
