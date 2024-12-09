using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Companies",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ParentId = table.Column<int>(type: "int", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Companies", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Companies_Companies_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Companies",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Contacts",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Contacts", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GroupFunctions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Type = table.Column<int>(type: "int", nullable: false),
            //        Order = table.Column<int>(type: "int", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GroupFunctions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Images",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDisplay = table.Column<bool>(type: "bit", nullable: false),
            //        Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Images", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NationalCostumeCategories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ParentId = table.Column<int>(type: "int", nullable: true),
            //        View = table.Column<int>(type: "int", nullable: false),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NationalCostumeCategories", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_NationalCostumeCategories_NationalCostumeCategories_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "NationalCostumeCategories",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NewsCategories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ParentId = table.Column<int>(type: "int", nullable: true),
            //        View = table.Column<int>(type: "int", nullable: false),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NewsCategories", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_NewsCategories_NewsCategories_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "NewsCategories",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PeopleCategories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ParentId = table.Column<int>(type: "int", nullable: true),
            //        View = table.Column<int>(type: "int", nullable: false),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PeopleCategories", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PeopleCategories_PeopleCategories_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "PeopleCategories",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Positions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Positions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Departments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DepartmentCompanyId = table.Column<int>(type: "int", nullable: true),
            //        ParentId = table.Column<int>(type: "int", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Departments", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Departments_Companies_DepartmentCompanyId",
            //            column: x => x.DepartmentCompanyId,
            //            principalTable: "Companies",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Departments_Departments_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Departments",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Functions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FunctionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        GroupId = table.Column<int>(type: "int", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Functions", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Functions_GroupFunctions_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "GroupFunctions",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "News",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Top = table.Column<bool>(type: "bit", nullable: false),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CategoryId = table.Column<int>(type: "int", nullable: true),
            //        Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Calendar = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        View = table.Column<int>(type: "int", nullable: false),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_News", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_News_NewsCategories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "NewsCategories",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Peoples",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDisplay = table.Column<bool>(type: "bit", nullable: false),
            //        Top = table.Column<bool>(type: "bit", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image360 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Population = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        History = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PeopleCategoryId = table.Column<int>(type: "int", nullable: true),
            //        ParentId = table.Column<int>(type: "int", nullable: true),
            //        Image0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Peoples", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Peoples_PeopleCategories_PeopleCategoryId",
            //            column: x => x.PeopleCategoryId,
            //            principalTable: "PeopleCategories",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Peoples_Peoples_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Peoples",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserCompanyId = table.Column<int>(type: "int", nullable: true),
            //        DepartmentCompanyId = table.Column<int>(type: "int", nullable: true),
            //        UserPositionId = table.Column<int>(type: "int", nullable: true),
            //        SuperAdmin = table.Column<bool>(type: "bit", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Users_Companies_UserCompanyId",
            //            column: x => x.UserCompanyId,
            //            principalTable: "Companies",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Users_Departments_DepartmentCompanyId",
            //            column: x => x.DepartmentCompanyId,
            //            principalTable: "Departments",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Users_Positions_UserPositionId",
            //            column: x => x.UserPositionId,
            //            principalTable: "Positions",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FunctionRole",
            //    columns: table => new
            //    {
            //        FunctionRolesId = table.Column<int>(type: "int", nullable: false),
            //        RoleFunctionsId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FunctionRole", x => new { x.FunctionRolesId, x.RoleFunctionsId });
            //        table.ForeignKey(
            //            name: "FK_FunctionRole_Functions_RoleFunctionsId",
            //            column: x => x.RoleFunctionsId,
            //            principalTable: "Functions",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_FunctionRole_Roles_FunctionRolesId",
            //            column: x => x.FunctionRolesId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Locations",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PeopleId = table.Column<int>(type: "int", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Locations", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Locations_Peoples_PeopleId",
            //            column: x => x.PeopleId,
            //            principalTable: "Peoples",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NationalCostumes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsDisplay = table.Column<bool>(type: "bit", nullable: false),
            //        Top = table.Column<bool>(type: "bit", nullable: false),
            //        Image360 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Shape = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Technique = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Classify = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Certification = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Cost = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryId = table.Column<int>(type: "int", nullable: true),
            //        PeopleId = table.Column<int>(type: "int", nullable: true),
            //        Image0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Image9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DeleteStatus = table.Column<int>(type: "int", nullable: false),
            //        InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NationalCostumes", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_NationalCostumes_NationalCostumeCategories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "NationalCostumeCategories",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_NationalCostumes_Peoples_PeopleId",
            //            column: x => x.PeopleId,
            //            principalTable: "Peoples",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RoleUser",
            //    columns: table => new
            //    {
            //        RoleUsersId = table.Column<int>(type: "int", nullable: false),
            //        UserRolesId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleUser", x => new { x.RoleUsersId, x.UserRolesId });
            //        table.ForeignKey(
            //            name: "FK_RoleUser_Roles_UserRolesId",
            //            column: x => x.UserRolesId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RoleUser_Users_RoleUsersId",
            //            column: x => x.RoleUsersId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Companies_ParentId",
            //    table: "Companies",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Departments_DepartmentCompanyId",
            //    table: "Departments",
            //    column: "DepartmentCompanyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Departments_ParentId",
            //    table: "Departments",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FunctionRole_RoleFunctionsId",
            //    table: "FunctionRole",
            //    column: "RoleFunctionsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Functions_GroupId",
            //    table: "Functions",
            //    column: "GroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Locations_PeopleId",
            //    table: "Locations",
            //    column: "PeopleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NationalCostumeCategories_ParentId",
            //    table: "NationalCostumeCategories",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NationalCostumes_CategoryId",
            //    table: "NationalCostumes",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NationalCostumes_PeopleId",
            //    table: "NationalCostumes",
            //    column: "PeopleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_News_CategoryId",
            //    table: "News",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NewsCategories_ParentId",
            //    table: "NewsCategories",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PeopleCategories_ParentId",
            //    table: "PeopleCategories",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Peoples_ParentId",
            //    table: "Peoples",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Peoples_PeopleCategoryId",
            //    table: "Peoples",
            //    column: "PeopleCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleUser_UserRolesId",
            //    table: "RoleUser",
            //    column: "UserRolesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_DepartmentCompanyId",
            //    table: "Users",
            //    column: "DepartmentCompanyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_UserCompanyId",
            //    table: "Users",
            //    column: "UserCompanyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_UserPositionId",
            //    table: "Users",
            //    column: "UserPositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Contacts");

            //migrationBuilder.DropTable(
            //    name: "FunctionRole");

            //migrationBuilder.DropTable(
            //    name: "Images");

            //migrationBuilder.DropTable(
            //    name: "Locations");

            //migrationBuilder.DropTable(
            //    name: "NationalCostumes");

            //migrationBuilder.DropTable(
            //    name: "News");

            //migrationBuilder.DropTable(
            //    name: "RoleUser");

            //migrationBuilder.DropTable(
            //    name: "Functions");

            //migrationBuilder.DropTable(
            //    name: "NationalCostumeCategories");

            //migrationBuilder.DropTable(
            //    name: "Peoples");

            //migrationBuilder.DropTable(
            //    name: "NewsCategories");

            //migrationBuilder.DropTable(
            //    name: "Roles");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "GroupFunctions");

            //migrationBuilder.DropTable(
            //    name: "PeopleCategories");

            //migrationBuilder.DropTable(
            //    name: "Departments");

            //migrationBuilder.DropTable(
            //    name: "Positions");

            //migrationBuilder.DropTable(
            //    name: "Companies");
        }
    }
}
