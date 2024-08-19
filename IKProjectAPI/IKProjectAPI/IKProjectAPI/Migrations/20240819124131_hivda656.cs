using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hivda656 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "27ab2a62-7740-421f-a5d4-38539e135b8a", new DateTime(2024, 8, 19, 15, 41, 30, 594, DateTimeKind.Local).AddTicks(1971), "AQAAAAIAAYagAAAAEBAzgGxLLEvsVGs9us7CPw2NTZ7wQgU1Vs4oL3okzHE2r9AZtZa7xs3Z/DOPpr8JPg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "d8839946-5369-4f58-acfb-38dac70a3102", new DateTime(2024, 8, 15, 12, 8, 7, 591, DateTimeKind.Local).AddTicks(5158), "AQAAAAIAAYagAAAAEEf9YbckX4peHf2upSrAS5JcJHr+MinPF+qloPXbQnMpHb/53yQGVi/ct1Kv6SAxKA==" });
        }
    }
}
