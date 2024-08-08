using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class hkNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_YoneticiId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_sirketler_SirketId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "SirketId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b33bcc1d-5441-403d-8e90-6c16e18ca714", "AQAAAAIAAYagAAAAEOMKg4160fhn3DX0RdPEOvhZz8zuKEmLTYg6MNiWiyLJbtWOljrTSmLEtFmvT0JeUQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SirketId1",
                table: "AspNetUsers",
                column: "SirketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_YoneticiId",
                table: "AspNetUsers",
                column: "YoneticiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_sirketler_SirketId",
                table: "AspNetUsers",
                column: "SirketId",
                principalTable: "sirketler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_sirketler_SirketId1",
                table: "AspNetUsers",
                column: "SirketId1",
                principalTable: "sirketler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_YoneticiId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_sirketler_SirketId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_sirketler_SirketId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SirketId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SirketId1",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "IdentityUser<int>",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d29bdc0b-98a7-486b-9186-0d0b4dc8015c", "AQAAAAIAAYagAAAAEEC7MYQf+NEHHZfvf1lMofvNzFdsfA9rXkym0gj5P6R5eLfdzzhTYYJIs7gH5RVQWA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_YoneticiId",
                table: "AspNetUsers",
                column: "YoneticiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_sirketler_SirketId",
                table: "AspNetUsers",
                column: "SirketId",
                principalTable: "sirketler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
