using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class AddProduceTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProduceToolCategories",
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
                    table.PrimaryKey("PK_ProduceToolCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduceToolCategories_ProduceToolCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProduceToolCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProduceTool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<bool>(type: "bit", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calendar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProduceToolCategoryId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeleteStatus = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduceTool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduceTool_NewsCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "NewsCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProduceTool_ProduceToolCategories_ProduceToolCategoryId",
                        column: x => x.ProduceToolCategoryId,
                        principalTable: "ProduceToolCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduceTool_CategoryId",
                table: "ProduceTool",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduceTool_ProduceToolCategoryId",
                table: "ProduceTool",
                column: "ProduceToolCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduceToolCategories_ParentId",
                table: "ProduceToolCategories",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduceTool");

            migrationBuilder.DropTable(
                name: "ProduceToolCategories");
        }
    }
}
