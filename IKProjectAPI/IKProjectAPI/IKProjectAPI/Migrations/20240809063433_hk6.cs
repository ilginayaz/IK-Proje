using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hk6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "18e8761e-47c8-4bd0-b903-ee9b271354bd", "AQAAAAIAAYagAAAAEP5ZrzzYOQ0OjHZ97xOaaoCkXqT1NlOdRuUe1ykPp++SWu3gGbvWGMcBzCxnSvA6Bg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64fb41f6-3cf9-4bd6-9894-078755379bfd", "AQAAAAIAAYagAAAAEFQsdhnF9xg17AwpbBKd1bqMLJZ8tHniqAPuQdrKUL+sxZR6dgbCaPL/4sEzMw155A==" });
        }
    }
}
