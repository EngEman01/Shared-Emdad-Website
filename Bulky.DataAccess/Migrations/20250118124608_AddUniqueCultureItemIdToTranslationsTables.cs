using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCultureItemIdToTranslationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTranslations_ProductId",
                table: "ProductTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslaltiosns_CategoryId",
                table: "CategoryTranslaltiosns");

            migrationBuilder.DropIndex(
                name: "IX_AdTranslations_AdvertisementId",
                table: "AdTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "ProductTranslations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "CategoryTranslaltiosns",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "AdTranslations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_ProductId_Language",
                table: "ProductTranslations",
                columns: new[] { "ProductId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslaltiosns_CategoryId_Language",
                table: "CategoryTranslaltiosns",
                columns: new[] { "CategoryId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdTranslations_AdvertisementId_Language",
                table: "AdTranslations",
                columns: new[] { "AdvertisementId", "Language" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTranslations_ProductId_Language",
                table: "ProductTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslaltiosns_CategoryId_Language",
                table: "CategoryTranslaltiosns");

            migrationBuilder.DropIndex(
                name: "IX_AdTranslations_AdvertisementId_Language",
                table: "AdTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "ProductTranslations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "CategoryTranslaltiosns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "AdTranslations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_ProductId",
                table: "ProductTranslations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslaltiosns_CategoryId",
                table: "CategoryTranslaltiosns",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdTranslations_AdvertisementId",
                table: "AdTranslations",
                column: "AdvertisementId");
        }
    }
}
