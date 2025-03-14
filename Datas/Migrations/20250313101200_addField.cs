using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class addField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativeNames",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnualVisitors",
                table: "Landmarks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Architecture",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingMaterial",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationProject",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulturalValue",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentCondition",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Exhibits",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExperienceActivities",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GpsCoordinates",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeritageRanking",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCode",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandmarkType",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagementAuthority",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpeningHours",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedEvents",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeScale",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpiritualBelief",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupportServices",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketPrice",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgeGroup",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativeNames",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommonRegions",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulturalImpact",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayedItems",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoricalChanges",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCode",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreservationMethod",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedEvents",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scan3DLink",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeWeight",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SymbolicMeaning",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativeNames",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommonRegions",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulturalValue",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayedItems",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoricalChanges",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCode",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainMaterial",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreservationMethod",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedCustoms",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedEvents",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scan3DLink",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeWeight",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Structure",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsageMethod",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserGroup",
                table: "DailyItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativeNames",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommonRegions",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulturalValue",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayedItems",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoricalChanges",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCode",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainMaterial",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreservationMethod",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedCustoms",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedRituals",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scan3DLink",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeWeight",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Structure",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsageCharacters",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsageMethod",
                table: "CeremonialTools",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "AlternativeNames",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "AnnualVisitors",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "Architecture",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "BuildingMaterial",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "ConservationProject",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "CulturalValue",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "CurrentCondition",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "Exhibits",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "ExperienceActivities",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "GpsCoordinates",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "HeritageRanking",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "IdentityCode",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "LandmarkType",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "ManagementAuthority",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "RelatedEvents",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "SizeScale",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "SpiritualBelief",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "SupportServices",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "AlternativeNames",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "CommonRegions",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "CulturalImpact",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "DisplayedItems",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "HistoricalChanges",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "IdentityCode",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "PreservationMethod",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "RelatedEvents",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "Scan3DLink",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "SizeWeight",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "SymbolicMeaning",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "AlternativeNames",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "CommonRegions",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "CulturalValue",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "DisplayedItems",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "HistoricalChanges",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "IdentityCode",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "MainMaterial",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "PreservationMethod",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "RelatedCustoms",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "RelatedEvents",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "Scan3DLink",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "SizeWeight",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "Structure",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "UsageMethod",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "UserGroup",
                table: "DailyItems");

            migrationBuilder.DropColumn(
                name: "AlternativeNames",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "CommonRegions",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "CulturalValue",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "DisplayedItems",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "HistoricalChanges",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "IdentityCode",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "MainMaterial",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "PreservationMethod",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "RelatedCustoms",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "RelatedRituals",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "Scan3DLink",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "SizeWeight",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "Structure",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "UsageCharacters",
                table: "CeremonialTools");

            migrationBuilder.DropColumn(
                name: "UsageMethod",
                table: "CeremonialTools");
        }
    }
}
