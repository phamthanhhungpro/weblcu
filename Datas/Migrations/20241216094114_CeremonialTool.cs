using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class CeremonialTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CeremonialToolId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CeremonialToolCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeleteStatus = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CeremonialToolCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CeremonialToolCategories_CeremonialToolCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CeremonialToolCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CeremonialTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeleteStatus = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    Top = table.Column<bool>(type: "bit", nullable: false),
                    Image360 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shape = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Technique = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Classify = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    View = table.Column<int>(type: "int", nullable: false),
                    PeopleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CeremonialTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CeremonialTools_CeremonialToolCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CeremonialToolCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CeremonialTools_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CeremonialToolId",
                table: "Attachments",
                column: "CeremonialToolId");

            migrationBuilder.CreateIndex(
                name: "IX_CeremonialToolCategories_ParentId",
                table: "CeremonialToolCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CeremonialTools_CategoryId",
                table: "CeremonialTools",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CeremonialTools_PeopleId",
                table: "CeremonialTools",
                column: "PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_CeremonialTools_CeremonialToolId",
                table: "Attachments",
                column: "CeremonialToolId",
                principalTable: "CeremonialTools",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_CeremonialTools_CeremonialToolId",
                table: "Attachments");

            migrationBuilder.DropTable(
                name: "CeremonialTools");

            migrationBuilder.DropTable(
                name: "CeremonialToolCategories");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_CeremonialToolId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CeremonialToolId",
                table: "Attachments");
        }
    }
}
