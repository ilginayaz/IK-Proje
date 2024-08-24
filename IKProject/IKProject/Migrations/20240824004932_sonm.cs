using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProject.Migrations
{
    /// <inheritdoc />
    public partial class sonm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cabbdc5b-9cec-4ad9-9190-6c9b43b766ad", "AQAAAAIAAYagAAAAEM9hxbBXbT2xhloTkFeDR16SgxGbO1oF9JGRttT5wiDVWAJjgOFgrx3XL9GP6LrpFA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "26282e46-2475-415b-96f0-817791aca3cd", "AQAAAAIAAYagAAAAEM+XUCqTTR/T4Li4awiYlbX/Jh7ZxYDjsYzyN95tDRQTm6DEIM0Hm+zr9P/3bRaMMw==" });
        }
    }
}
