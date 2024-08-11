using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hk7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e15f649-9b15-4665-9a77-400c814afee5", "AQAAAAIAAYagAAAAEKhEOVRLeV5PRWfVih7OOIxazTMOMYQ/HveLNj153WhL3OurDnxXb08RNjekCCruRw==" });
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
