using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatePeopleContentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content1",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content2",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content3",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content4",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content5",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content1",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "Content2",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "Content3",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "Content4",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "Content5",
                table: "Peoples");
        }
    }
}
