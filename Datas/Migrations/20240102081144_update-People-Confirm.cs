using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class updatePeopleConfirm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeopleConfirm_Peoples_PeopleId",
                table: "PeopleConfirm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeopleConfirm",
                table: "PeopleConfirm");

            migrationBuilder.RenameTable(
                name: "PeopleConfirm",
                newName: "PeopleConfirmes");

            migrationBuilder.RenameIndex(
                name: "IX_PeopleConfirm_PeopleId",
                table: "PeopleConfirmes",
                newName: "IX_PeopleConfirmes_PeopleId");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmStatus",
                table: "Peoples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeopleConfirmes",
                table: "PeopleConfirmes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleConfirmes_Peoples_PeopleId",
                table: "PeopleConfirmes",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeopleConfirmes_Peoples_PeopleId",
                table: "PeopleConfirmes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeopleConfirmes",
                table: "PeopleConfirmes");

            migrationBuilder.DropColumn(
                name: "ConfirmStatus",
                table: "Peoples");

            migrationBuilder.RenameTable(
                name: "PeopleConfirmes",
                newName: "PeopleConfirm");

            migrationBuilder.RenameIndex(
                name: "IX_PeopleConfirmes_PeopleId",
                table: "PeopleConfirm",
                newName: "IX_PeopleConfirm_PeopleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeopleConfirm",
                table: "PeopleConfirm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleConfirm_Peoples_PeopleId",
                table: "PeopleConfirm",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id");
        }
    }
}
