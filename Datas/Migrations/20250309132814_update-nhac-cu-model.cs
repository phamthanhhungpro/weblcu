using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatenhaccumodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CulturalSignificance",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FamousArtisans",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformanceEvents",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PitchRange",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayingMethod",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleInOrchestra",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Structure",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Timbre",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VungMien",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CulturalSignificance",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "FamousArtisans",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "PerformanceEvents",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "PitchRange",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "PlayingMethod",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "RoleInOrchestra",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Structure",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Timbre",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "VungMien",
                table: "Instruments");
        }
    }
}
