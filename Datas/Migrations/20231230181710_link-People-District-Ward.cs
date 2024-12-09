using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class linkPeopleDistrictWard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Peoples",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Peoples",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_DistrictId",
                table: "Peoples",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_WardId",
                table: "Peoples",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Districts_DistrictId",
                table: "Peoples",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Wards_WardId",
                table: "Peoples",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Districts_DistrictId",
                table: "Peoples");

            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Wards_WardId",
                table: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_Peoples_DistrictId",
                table: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_Peoples_WardId",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Peoples");
        }
    }
}
