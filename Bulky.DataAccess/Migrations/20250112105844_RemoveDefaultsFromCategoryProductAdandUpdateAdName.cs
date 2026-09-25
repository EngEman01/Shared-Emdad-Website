using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDefaultsFromCategoryProductAdandUpdateAdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DefaultName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DefaultDescription",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DefaultName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DefaultDescription",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "DefaultTitle",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AdTranslations",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AdTranslations",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "DefaultDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultDescription",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultDescription",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultTitle",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Category Default Description", "Defence Industries" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Category Default Description", "Civil Industries" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Category Default Description", "Medical Equipment" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Category Default Description", "Sports Equipment" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Category Default Description", "Physical Therapy Devices" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DefaultDescription", "DefaultName" },
                values: new object[] { "Default Product Description", "Default Product Name" });
        }
    }
}
