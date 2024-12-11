using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class EditProduceTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyWord",
                table: "ProduceTools",
                newName: "Technique");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ProduceTools",
                newName: "Size");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certification",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classify",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cost",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatus",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Event",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image0",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image360",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image9",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisplay",
                table: "ProduceTools",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Top",
                table: "ProduceTools",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProduceToolId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ProduceToolId",
                table: "Attachments",
                column: "ProduceToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_ProduceTools_ProduceToolId",
                table: "Attachments",
                column: "ProduceToolId",
                principalTable: "ProduceTools",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_ProduceTools_ProduceToolId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_ProduceToolId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Certification",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Classify",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Event",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image0",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image360",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image6",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image7",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image8",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Image9",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "IsDisplay",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "Top",
                table: "ProduceTools");

            migrationBuilder.DropColumn(
                name: "ProduceToolId",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "Technique",
                table: "ProduceTools",
                newName: "KeyWord");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ProduceTools",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ProduceTools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
