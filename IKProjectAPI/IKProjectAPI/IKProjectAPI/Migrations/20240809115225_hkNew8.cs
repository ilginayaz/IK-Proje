using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hkNew8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2882dbdd-3807-442e-88ba-2332bf37169c", "AQAAAAIAAYagAAAAEHp4WDWEToP4iSUoldg97gNl0YGlGtbPG0EDEc4ZnjO/bXR64Ps789KnoiZ7whMdWg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "18e8761e-47c8-4bd0-b903-ee9b271354bd", "AQAAAAIAAYagAAAAEP5ZrzzYOQ0OjHZ97xOaaoCkXqT1NlOdRuUe1ykPp++SWu3gGbvWGMcBzCxnSvA6Bg==" });
        }
    }
}
