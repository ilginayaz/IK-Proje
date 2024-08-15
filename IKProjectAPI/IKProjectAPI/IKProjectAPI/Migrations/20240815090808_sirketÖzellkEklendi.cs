using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class sirketÖzellkEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e1fd9964-2c65-486e-83c5-4743fe5a819d" });

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "sirketler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SirketUnvani",
                table: "sirketler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "sirketler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e1fd9964-2c65-486e-83c5-4743fe5a819c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "d8839946-5369-4f58-acfb-38dac70a3102", new DateTime(2024, 8, 15, 12, 8, 7, 591, DateTimeKind.Local).AddTicks(5158), "AQAAAAIAAYagAAAAEEf9YbckX4peHf2upSrAS5JcJHr+MinPF+qloPXbQnMpHb/53yQGVi/ct1Kv6SAxKA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e1fd9964-2c65-486e-83c5-4743fe5a819c" });

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "sirketler");

            migrationBuilder.DropColumn(
                name: "SirketUnvani",
                table: "sirketler");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "sirketler");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e1fd9964-2c65-486e-83c5-4743fe5a819d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "c2b814cb-d170-4deb-aa55-cd8e2b9273fd", new DateTime(2024, 8, 15, 9, 59, 37, 901, DateTimeKind.Local).AddTicks(1333), "AQAAAAIAAYagAAAAEM0+ZbFEBO4maKKnkltN9DN6yNnZUw5cyeUnWAxn4GDJsvUbWLw0B/HEhY78QS5ILA==" });
        }
    }
}
