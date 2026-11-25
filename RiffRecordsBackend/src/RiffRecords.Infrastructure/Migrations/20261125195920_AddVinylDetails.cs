using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiffRecords.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVinylDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Track_VinylId",
                table: "Track");

            // Year (the album's year) is not the track's number: drop it and add a new column
            // instead of renaming, so old years never end up as track numbers.
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Track");

            migrationBuilder.AddColumn<int>(
                name: "TrackNumber",
                table: "Track",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Vinyls",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vinyls",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Vinyls",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RuntimeSeconds",
                table: "Vinyls",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationSeconds",
                table: "Track",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFamous",
                table: "Track",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Track_VinylId_TrackNumber",
                table: "Track",
                columns: new[] { "VinylId", "TrackNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_OneFamousPerVinyl",
                table: "Track",
                column: "VinylId",
                unique: true,
                filter: "[IsFamous] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Track_VinylId_TrackNumber",
                table: "Track");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_OneFamousPerVinyl",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vinyls");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Vinyls");

            migrationBuilder.DropColumn(
                name: "RuntimeSeconds",
                table: "Vinyls");

            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "IsFamous",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "TrackNumber",
                table: "Track");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Track",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Vinyls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Track_VinylId",
                table: "Track",
                column: "VinylId");
        }
    }
}
