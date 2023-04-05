using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class components : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComponentName",
                table: "Components",
                newName: "PageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ComModuleName",
                table: "Components",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");


            migrationBuilder.AddColumn<string>(
                name: "ModuleImage",
                table: "Components",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PageDescription",
                table: "Components",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PageName",
                table: "Components",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PageTitle",
                table: "Components",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentComponentId",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComModuleName",
                table: "Components");

           

            migrationBuilder.DropColumn(
                name: "ModuleImage",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PageDescription",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PageName",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PageTitle",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ParentComponentId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "PageUrl",
                table: "Components",
                newName: "ComponentName");
        }
    }
}
