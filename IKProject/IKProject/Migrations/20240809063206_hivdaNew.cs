using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProject.Migrations
{
    /// <inheritdoc />
    public partial class hivdaNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "08d77e7d-ce68-4b69-a090-002c638edad0", "AQAAAAIAAYagAAAAEJ9FwfkX+bjkP2An52NCpD1gJJ23mfVmwZjiz+k0At9ADcMl3pZm08zdBRdfG1eyjg==" });
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
