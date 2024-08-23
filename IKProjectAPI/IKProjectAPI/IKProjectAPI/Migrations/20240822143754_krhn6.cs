using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class krhn6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "62520c32-db50-4fcf-b7c7-c93a07f5c582", new DateTime(2024, 8, 22, 17, 37, 53, 472, DateTimeKind.Local).AddTicks(7906), "AQAAAAIAAYagAAAAEHhSXT2MwTIVKdKnHprjXOvYVi2FzwY4ZaN4CmfTpSfpQqLcdoTGI+5kct0R46DyvQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "91f7f9b7-359a-4e1e-9c5e-36d83031190e", new DateTime(2024, 8, 22, 1, 16, 44, 971, DateTimeKind.Local).AddTicks(50), "AQAAAAIAAYagAAAAEJn4gCX7ZK8zjtQdwUuhfmTwQztmZimGa36El6aqWRWal+eZtKMG641Dap+lGIlnfA==" });
        }
    }
}
