using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatepttq1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenGoiKhac",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenGoiKhac",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangesOverTime",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationStatus",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationUnit",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormOfImplementation",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreservationActivities",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurposeMeaning",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedBeliefs",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredItems",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialImpact",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Steps",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenGoiKhac",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeOfOccurrence",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraditionalAttire",
                table: "CustomsTraditions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenGoiKhac",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "TenGoiKhac",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "ChangesOverTime",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "ConservationStatus",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "ConservationUnit",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "FormOfImplementation",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "Participants",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "PreservationActivities",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "PurposeMeaning",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "RelatedBeliefs",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "RequiredItems",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "SocialImpact",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "Steps",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "TenGoiKhac",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "TimeOfOccurrence",
                table: "CustomsTraditions");

            migrationBuilder.DropColumn(
                name: "TraditionalAttire",
                table: "CustomsTraditions");
        }
    }
}
