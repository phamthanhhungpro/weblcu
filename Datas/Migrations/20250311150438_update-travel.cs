using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatetravel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccompanyingEvents",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompanyingServices",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulturalSocialImpact",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailedDescription",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaDiem",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountsAndPromotions",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViToChuc",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Highlights",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiHinh",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagingAgency",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperationalStatus",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipationCost",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionalActivities",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredAttireAndEquipment",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suitability",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThoiGian",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueExperiences",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccompanyingEvents",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "AccompanyingServices",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "CulturalSocialImpact",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "DetailedDescription",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "DiaDiem",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "DiscountsAndPromotions",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "DonViToChuc",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "Highlights",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "LoaiHinh",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "ManagingAgency",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "OperationalStatus",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "ParticipationCost",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "PromotionalActivities",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "RequiredAttireAndEquipment",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "Suitability",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "ThoiGian",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "UniqueExperiences",
                table: "Travels");
        }
    }
}
