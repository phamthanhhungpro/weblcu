using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatePeopleLang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "LanguagePeople",
                columns: table => new
                {
                    LanguagesId = table.Column<int>(type: "int", nullable: false),
                    PeoplesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagePeople", x => new { x.LanguagesId, x.PeoplesId });
                    table.ForeignKey(
                        name: "FK_LanguagePeople_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagePeople_Peoples_PeoplesId",
                        column: x => x.PeoplesId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguagePeople_PeoplesId",
                table: "LanguagePeople",
                column: "PeoplesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguagePeople");

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
    }
}
