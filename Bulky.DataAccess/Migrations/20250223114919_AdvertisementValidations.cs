using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementValidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7473cefb-d91c-466b-b7bc-20c061bc6cfe", "AQAAAAIAAYagAAAAEDlH+9Yom84Xd6xSow831PR/cbtqnDlkFIE5I14fA3Y6X56PMfnm901JaXo//UqVnA==", "2cd843ce-8541-4bbc-bf61-1823c99b1bb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2bd0a80-9127-4200-a5eb-5b55207d3768", "AQAAAAIAAYagAAAAEOIeaSR/6/HcgUBH8iA0Ja51wixHnTaOUw6Sj1k5Sld9H4nuVz4+ZhuXEkl64nZXhA==", "2cc9a3f8-6a5e-4abd-ba8e-934b2aed7f5e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "989ba847-1eca-4991-a5a3-6e505cdb9a0e", "AQAAAAIAAYagAAAAEETzeVwm/aP9uZDk+cHYpwZdzqtUMDZUAjfbH4QiAGpr0J5K70acq3f9Tti85TTFoQ==", "1f5ac702-bc00-4309-bf09-9001f267d3ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72a7fd81-5892-4ffd-8c96-0d78a7193fd8", "AQAAAAIAAYagAAAAEPcYgYvQHUsW8JZ9AJDf2GLP47vY8GQOYSIw2lV5CRdTPRcYF4Ris2WKI8ms6sMygQ==", "daf2de34-483d-4929-8d8a-979f4f1b30df" });
        }
    }
}
