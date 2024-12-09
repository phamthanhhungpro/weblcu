using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class addLinkLangPeople : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Peoples",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_LanguageId",
                table: "Peoples",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Languages_LanguageId",
                table: "Peoples",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Languages_LanguageId",
                table: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_Peoples_LanguageId",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Peoples");
        }
    }
}
