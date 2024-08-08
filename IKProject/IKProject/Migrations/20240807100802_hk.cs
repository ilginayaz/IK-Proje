﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProject.Migrations
{
    /// <inheritdoc />
    public partial class hk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9d7d8a40-d761-4f2f-aeaf-d39fe6882ccc", "AQAAAAIAAYagAAAAEA6zwjZt1NQ2OQ0e1Y/Sj5Lt4pjNOE0bG5BgwKqwIj7pkQvLbwjTHF4JCqTAS2tWoA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5156028-b772-4b1a-9d85-f9b27ea614ff", "AQAAAAIAAYagAAAAECWmNcm7R8/YV4Ooyu1qTiWKDzrTTW+An3gunOOO+uhSNDhnLwJcmD9qjuDzC2FRfA==" });
        }
    }
}
