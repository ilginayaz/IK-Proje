using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hkNew2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64fb41f6-3cf9-4bd6-9894-078755379bfd", "AQAAAAIAAYagAAAAEFQsdhnF9xg17AwpbBKd1bqMLJZ8tHniqAPuQdrKUL+sxZR6dgbCaPL/4sEzMw155A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b33bcc1d-5441-403d-8e90-6c16e18ca714", "AQAAAAIAAYagAAAAEOMKg4160fhn3DX0RdPEOvhZz8zuKEmLTYg6MNiWiyLJbtWOljrTSmLEtFmvT0JeUQ==" });
        }
    }
}
