using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hkNew66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f66c12cd-a091-4983-8c56-128e72608a09", "AQAAAAIAAYagAAAAEFBX/wEhuqRFK/FMw9DDD2U8ELD3ClQJAVH7noydpjoDUAE5XK4ISvRuPw+sh8+N+w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2882dbdd-3807-442e-88ba-2332bf37169c", "AQAAAAIAAYagAAAAEHp4WDWEToP4iSUoldg97gNl0YGlGtbPG0EDEc4ZnjO/bXR64Ps789KnoiZ7whMdWg==" });
        }
    }
}
