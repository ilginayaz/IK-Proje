using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class adminBilgileri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819d",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "81ca39bd-d8be-4ed0-a31d-961e6743beb8", new DateTime(2024, 8, 15, 9, 45, 57, 209, DateTimeKind.Local).AddTicks(113), "AQAAAAIAAYagAAAAEFEVFVgxSF+KOQEIhuQZWE6Lq8Eso7TjHuQ3Px3AcUCFD+TPoynUdEuOXmkIO5sp8Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819d",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "0cea83b5-2bc3-4335-b822-bd8cdb82d366", new DateTime(2024, 8, 15, 0, 11, 0, 711, DateTimeKind.Local).AddTicks(9486), "AQAAAAIAAYagAAAAEE9oaxkSoiwpkwZEBCYw8x0UlTrbzNfvGrBOUZdZ5/N9deM7Fqo27FqO3Y5GMhsISQ==" });
        }
    }
}
