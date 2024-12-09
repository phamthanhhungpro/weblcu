using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updateDistrictWard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("UPDATE Peoples SET DistrictId = NULL,WardId = NULL");
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

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DistrictId",
                table: "Locations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_WardId",
                table: "Locations",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Wards_WardId",
                table: "Locations",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Wards_WardId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_DistrictId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_WardId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Locations");

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
    }
}
