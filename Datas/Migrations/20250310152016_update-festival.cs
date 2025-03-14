using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatefestival : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangesOverTime",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationActivities",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationStatus",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulturalSocialImpact",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FolkGames",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainEvents",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainRituals",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferingsAndWorship",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationForm",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizingAgency",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedBeliefs",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TTBaoTon",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenGoiKhac",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThoiGianToChuc",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraditionalCostumes",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VungMien",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangesOverTime",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "ConservationActivities",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "ConservationStatus",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "CulturalSocialImpact",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "FolkGames",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "MainEvents",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "MainRituals",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "OfferingsAndWorship",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "OrganizationForm",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "OrganizingAgency",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "RelatedBeliefs",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "TTBaoTon",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "TenGoiKhac",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "ThoiGianToChuc",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "TraditionalCostumes",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "VungMien",
                table: "Festivals");
        }
    }
}
