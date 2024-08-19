using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class talepler1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "b6dc9d32-219f-4409-af18-62c919698dda", new DateTime(2024, 8, 19, 17, 14, 46, 668, DateTimeKind.Local).AddTicks(8197), "AQAAAAIAAYagAAAAEHp6zvpdeNsuKOJM9epCAMNNl4/b6qv2jWFpkFsD7hZKlAiFKFLHg1KCGy7Sb6k90g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "d8bd58c5-6a42-4a6e-a0b3-1413a22472bf", new DateTime(2024, 8, 19, 16, 57, 39, 838, DateTimeKind.Local).AddTicks(9216), "AQAAAAIAAYagAAAAEBamkcoUxVV2E2344xHtJ4DvW+bd5Ym//FBj6jWlj68AYiISkkdRpdPZc0opiYMlqQ==" });
        }
    }
}
