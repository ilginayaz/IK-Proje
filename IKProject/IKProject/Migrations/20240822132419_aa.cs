using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProject.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "26282e46-2475-415b-96f0-817791aca3cd", "AQAAAAIAAYagAAAAEM+XUCqTTR/T4Li4awiYlbX/Jh7ZxYDjsYzyN95tDRQTm6DEIM0Hm+zr9P/3bRaMMw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9d7d8a40-d761-4f2f-aeaf-d39fe6882ccc", "AQAAAAIAAYagAAAAEA6zwjZt1NQ2OQ0e1Y/Sj5Lt4pjNOE0bG5BgwKqwIj7pkQvLbwjTHF4JCqTAS2tWoA==" });
        }
    }
}
