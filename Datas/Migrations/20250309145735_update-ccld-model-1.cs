using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updateccldmodel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CulturalSignificance",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Custodian",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EconomicValue",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamousArtisans",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainFunction",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedTraditionalCrafts",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Structure",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHienTai",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsageEfficiency",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Variants",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CulturalSignificance",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Custodian",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "EconomicValue",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "FamousArtisans",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "MainFunction",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "RelatedTraditionalCrafts",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Structure",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "TinhTrangHienTai",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "UsageEfficiency",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Variants",
                table: "ProduceTools");
        }
    }
}
