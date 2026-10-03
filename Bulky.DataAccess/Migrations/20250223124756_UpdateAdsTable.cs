using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c30b229e-0523-4e1a-9e8c-7374458332a6", "AQAAAAIAAYagAAAAEJ2TM9jrIY2fFljXZUphVYlLQ2GBse3AImAZhP12F4CjjG710FWUDAlOFRAWz8tWOw==", "d15a9b36-493f-45d5-b6c6-6a31bb1a14b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90bcbd41-4fd7-48f9-82ff-27428e46cfcb", "AQAAAAIAAYagAAAAEM6zlkTnYZ3mrOe+OJ3Lo4FD3evtLTe6EbwE8p048QMv/MZRRRmeBRuVwg2OWerx/g==", "b007ccde-5169-460f-89b6-bd53c9ed1ad7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
