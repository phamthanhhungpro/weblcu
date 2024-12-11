using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class AddProduceTool1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduceTool_NewsCategories_CategoryId",
                table: "ProduceTool");

            migrationBuilder.DropForeignKey(
                name: "FK_ProduceTool_ProduceToolCategories_ProduceToolCategoryId",
                table: "ProduceTool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProduceTool",
                table: "ProduceTool");

            migrationBuilder.DropIndex(
                name: "IX_ProduceTool_ProduceToolCategoryId",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Calendar",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "ProduceToolCategoryId",
                table: "ProduceTool");

            migrationBuilder.DropColumn(
                name: "Top",
                table: "ProduceTool");

            migrationBuilder.RenameTable(
                name: "ProduceTool",
                newName: "ProduceTools");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProduceTools",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_ProduceTool_CategoryId",
                table: "ProduceTools",
                newName: "IX_ProduceTools_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProduceTools",
                table: "ProduceTools",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduceTools_ProduceToolCategories_CategoryId",
                table: "ProduceTools",
                column: "CategoryId",
                principalTable: "ProduceToolCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduceTools_ProduceToolCategories_CategoryId",
                table: "ProduceTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProduceTools",
                table: "ProduceTools");

            migrationBuilder.RenameTable(
                name: "ProduceTools",
                newName: "ProduceTool");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProduceTool",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_ProduceTools_CategoryId",
                table: "ProduceTool",
                newName: "IX_ProduceTool_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calendar",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ProduceTool",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "ProduceTool",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProduceToolCategoryId",
                table: "ProduceTool",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Top",
                table: "ProduceTool",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProduceTool",
                table: "ProduceTool",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProduceTool_ProduceToolCategoryId",
                table: "ProduceTool",
                column: "ProduceToolCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduceTool_NewsCategories_CategoryId",
                table: "ProduceTool",
                column: "CategoryId",
                principalTable: "NewsCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduceTool_ProduceToolCategories_ProduceToolCategoryId",
                table: "ProduceTool",
                column: "ProduceToolCategoryId",
                principalTable: "ProduceToolCategories",
                principalColumn: "Id");
        }
    }
}
