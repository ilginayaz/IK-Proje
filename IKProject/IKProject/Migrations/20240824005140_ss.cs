using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProject.Migrations
{
    /// <inheritdoc />
    public partial class ss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce9b5efa-c3b8-417c-881c-d14358c77a91", "AQAAAAIAAYagAAAAELKG0eRENUMfOC0lVY4lr2G1XBhU0teBQSvKqkOAjGfqArb5snBc0k4SxAiGcyn5fQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cabbdc5b-9cec-4ad9-9190-6c9b43b766ad", "AQAAAAIAAYagAAAAEM9hxbBXbT2xhloTkFeDR16SgxGbO1oF9JGRttT5wiDVWAJjgOFgrx3XL9GP6LrpFA==" });
        }
    }
}
