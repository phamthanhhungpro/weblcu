using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstrument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Instruments");

            migrationBuilder.RenameColumn(
                name: "KeyWord",
                table: "Instruments",
                newName: "Technique");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Instruments",
                newName: "Size");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certification",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classify",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cost",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatus",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Event",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image0",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image360",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image9",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisplay",
                table: "Instruments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeopleId",
                table: "Instruments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstrumentId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_PeopleId",
                table: "Instruments",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_InstrumentId",
                table: "Attachments",
                column: "InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Instruments_InstrumentId",
                table: "Attachments",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_Peoples_PeopleId",
                table: "Instruments",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Instruments_InstrumentId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_Peoples_PeopleId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_PeopleId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_InstrumentId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Certification",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Classify",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Event",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image0",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image360",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image6",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image7",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image8",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Image9",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "IsDisplay",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "InstrumentId",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "Technique",
                table: "Instruments",
                newName: "KeyWord");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Instruments",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Instruments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
