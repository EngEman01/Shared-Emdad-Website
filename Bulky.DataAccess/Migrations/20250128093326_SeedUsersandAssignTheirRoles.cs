using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersandAssignTheirRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "Discriminator", "Email", "EmailConfirmed", "FullName", "HomeAddress", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, null, "b3e74428-7706-4076-a9c4-3e48cc5040e3", null, "AppUser", "info@eltamawiya-emdad.com", false, null, null, false, null, "INFO@ELTAMAWIYA-EMDAD.COM", "ADMIN EMDAD", "AQAAAAIAAYagAAAAEHGNzxi5z3Jg3RKOW03DpYyZUZ4iEgMdroW1iFEpqkiRxWdm+9kcKy8BquPeYQJ9tQ==", null, false, null, "bb92ae59-d1c4-4c19-91fd-48a02c9a20ea", false, "Admin Emdad" },
                    { "2", 0, null, "2653055a-532f-41b1-849b-c4035f1d7411", null, "AppUser", "emdadindustries@eltamawiya-emdad.com", false, null, null, false, null, "EMDADINDUSTRIES@ELTAMAWIYA-EMDAD.COM", "USER EMDAD", "AQAAAAIAAYagAAAAEOSjPg2eIN2mMqOwVMNOtl6bJRaNHmVg42Rq600yYR6RkgKDn8QzNXdzACwVpLhF5Q==", null, false, null, "cded8dc5-25ee-4fe1-a695-e468470904a3", false, "User Emdad" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
