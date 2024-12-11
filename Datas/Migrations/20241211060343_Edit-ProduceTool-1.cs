using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class EditProduceTool1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeopleId",
                table: "ProduceTools",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProduceTools_PeopleId",
                table: "ProduceTools",
                column: "PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduceTools_Peoples_PeopleId",
                table: "ProduceTools",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduceTools_Peoples_PeopleId",
                table: "ProduceTools");

            migrationBuilder.DropIndex(
                name: "IX_ProduceTools_PeopleId",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "ProduceTools");
        }
    }
}
