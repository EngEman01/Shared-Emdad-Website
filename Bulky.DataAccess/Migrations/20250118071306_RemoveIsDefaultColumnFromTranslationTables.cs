using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsDefaultColumnFromTranslationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ProductTranslations");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "CategoryTranslaltiosns");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AdTranslations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ProductTranslations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "CategoryTranslaltiosns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AdTranslations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsDefault",
                value: false);
        }
    }
}
