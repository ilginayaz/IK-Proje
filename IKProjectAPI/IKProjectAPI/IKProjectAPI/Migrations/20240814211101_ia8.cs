using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class ia8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819d",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cea83b5-2bc3-4335-b822-bd8cdb82d366", new DateTime(2024, 8, 15, 0, 11, 0, 711, DateTimeKind.Local).AddTicks(9486), "AQAAAAIAAYagAAAAEE9oaxkSoiwpkwZEBCYw8x0UlTrbzNfvGrBOUZdZ5/N9deM7Fqo27FqO3Y5GMhsISQ==", "8678980EED564CECB09BD68613AC7382" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819d",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19cba33f-d130-4b5d-a243-39c51ed04fbd", new DateTime(2024, 8, 14, 23, 51, 29, 318, DateTimeKind.Local).AddTicks(1599), "AQAAAAIAAYagAAAAEKR6u+OZsPTVAHsxHhthZkzVOCfdiGW3xUmlP2u+giQYq88uLJbvicZVDFwFw4a5JA==", "{8678980E-ED56-4CEC-B09B-D68613AC7382}" });
        }
    }
}
