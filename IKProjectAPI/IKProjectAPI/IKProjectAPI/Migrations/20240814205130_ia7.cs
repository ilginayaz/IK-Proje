using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class ia7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUser<int>");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<int>");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adi", "Adres", "Cinsiyet", "ConcurrencyStamp", "CreatedTime", "DeletedTime", "Departman", "DogumTarihi", "DogumYeri", "Email", "EmailConfirmed", "IkinciAdi", "IkinciSoyadi", "IseGirisTarihi", "IstenCikisTarihi", "LockoutEnabled", "LockoutEnd", "Maas", "Meslek", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhoto", "SecurityStamp", "SirketId", "SirketId1", "Soyadi", "Status", "TC", "Token", "TwoFactorEnabled", "UpdatedTime", "UserName", "YoneticiId" },
                values: new object[] { "e1fd9964-2c65-486e-83c5-4743fe5a819d", 0, "Admin", "Ilgın Mahallesi, Hivda Sokak, No:1, Ankara", 1, "19cba33f-d130-4b5d-a243-39c51ed04fbd", new DateTime(2024, 8, 14, 23, 51, 29, 318, DateTimeKind.Local).AddTicks(1599), null, 0, new DateOnly(1997, 1, 1), 5, "admin@admin.com", true, null, null, new DateOnly(2024, 1, 1), new DateOnly(1, 1, 1), false, null, 10000m, 0, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEKR6u+OZsPTVAHsxHhthZkzVOCfdiGW3xUmlP2u+giQYq88uLJbvicZVDFwFw4a5JA==", null, false, "https://randomuser.me/api/portraits/men/11.jpg", "{8678980E-ED56-4CEC-B09B-D68613AC7382}", new Guid("7d06417e-6ccf-4680-4462-08dcb8609bcc"), null, "User", 3, "12345678901", null, false, null, "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e1fd9964-2c65-486e-83c5-4743fe5a819d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e1fd9964-2c65-486e-83c5-4743fe5a819d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819d");

            migrationBuilder.CreateTable(
                name: "IdentityUser<int>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser<int>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<int>",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<int>", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.InsertData(
                table: "IdentityUser<int>",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "f66c12cd-a091-4983-8c56-128e72608a09", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEFBX/wEhuqRFK/FMw9DDD2U8ELD3ClQJAVH7noydpjoDUAE5XK4ISvRuPw+sh8+N+w==", null, false, "{8678980E-ED56-4CEC-B09B-D68613AC7382}", false, "admin" });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<int>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }
    }
}
