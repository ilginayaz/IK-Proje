using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class sirketIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SirketId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "91f7f9b7-359a-4e1e-9c5e-36d83031190e", new DateTime(2024, 8, 22, 1, 16, 44, 971, DateTimeKind.Local).AddTicks(50), "AQAAAAIAAYagAAAAEJn4gCX7ZK8zjtQdwUuhfmTwQztmZimGa36El6aqWRWal+eZtKMG641Dap+lGIlnfA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SirketId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "b6dc9d32-219f-4409-af18-62c919698dda", new DateTime(2024, 8, 19, 17, 14, 46, 668, DateTimeKind.Local).AddTicks(8197), "AQAAAAIAAYagAAAAEHp6zvpdeNsuKOJM9epCAMNNl4/b6qv2jWFpkFsD7hZKlAiFKFLHg1KCGy7Sb6k90g==" });
        }
    }
}
