using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hkSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f317f3d4-5d1b-4c90-966c-0f1cae6940e9", "AQAAAAIAAYagAAAAEDHFAWTbFuwoytgXicNYWwpE4b1u3JnApfbQXoORrg2f0Xs5LWk8KwucCVMAmxfOgQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d29bdc0b-98a7-486b-9186-0d0b4dc8015c", "AQAAAAIAAYagAAAAEEC7MYQf+NEHHZfvf1lMofvNzFdsfA9rXkym0gj5P6R5eLfdzzhTYYJIs7gH5RVQWA==" });
        }
    }
}
