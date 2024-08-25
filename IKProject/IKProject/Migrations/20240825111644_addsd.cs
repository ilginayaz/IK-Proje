using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProject.Migrations
{
    /// <inheritdoc />
    public partial class addsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05cbb741-6555-421d-a8ab-796cefb04992", "AQAAAAIAAYagAAAAEHZaUH08RMGrz/UD+Ousn5yTcM3LM8XK7ojjjCw0u8ugc7fdv9ErswMQhhJoV9WMOQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce9b5efa-c3b8-417c-881c-d14358c77a91", "AQAAAAIAAYagAAAAELKG0eRENUMfOC0lVY4lr2G1XBhU0teBQSvKqkOAjGfqArb5snBc0k4SxAiGcyn5fQ==" });
        }
    }
}
