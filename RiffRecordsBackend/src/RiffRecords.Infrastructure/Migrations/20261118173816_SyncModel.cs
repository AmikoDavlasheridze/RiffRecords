using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiffRecords.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Track_Bands_BandId",
                table: "Track");

            migrationBuilder.DropIndex(
                name: "IX_Track_BandId",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Track");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BandId",
                table: "Track",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Track_BandId",
                table: "Track",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Bands_BandId",
                table: "Track",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId");
        }
    }
}
