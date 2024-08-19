﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class adminBilgi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adi", "Adres", "Cinsiyet", "ConcurrencyStamp", "CreatedTime", "DeletedTime", "Departman", "DogumTarihi", "DogumYeri", "Email", "EmailConfirmed", "IkinciAdi", "IkinciSoyadi", "IseGirisTarihi", "IstenCikisTarihi", "LockoutEnabled", "LockoutEnd", "Maas", "Meslek", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhoto", "SecurityStamp", "SirketId", "SirketId1", "Soyadi", "Status", "TC", "Token", "TwoFactorEnabled", "UpdatedTime", "UserName", "YoneticiId" },
                values: new object[] { "e1fd9964-2c65-486e-83c5-4743fe5a819a", 0, "Admin", "Ilgın Mahallesi, Hivda Sokak, No:1, Ankara", 1, "2e60e4f1-6564-4f7f-9e6d-dd96ec33c30c", new DateTime(2024, 8, 15, 9, 51, 45, 293, DateTimeKind.Local).AddTicks(5177), null, 0, new DateOnly(1997, 1, 1), 5, "admin@admin.com", true, null, null, new DateOnly(2024, 1, 1), new DateOnly(1, 1, 1), false, null, 10000m, 0, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEB/JQDPXoSs16XL02xTStfTFTuWNE7ZXiO4/6OXmAcTqLndS2osVoAimLjbOkVaTUg==", null, false, "https://randomuser.me/api/portraits/men/11.jpg", "8678980EED564CECB09BD68613AC7382", new Guid("7d06417e-6ccf-4680-4462-08dcb8609bcc"), null, "User", 3, "12345678901", null, false, null, "admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adi", "Adres", "Cinsiyet", "ConcurrencyStamp", "CreatedTime", "DeletedTime", "Departman", "DogumTarihi", "DogumYeri", "Email", "EmailConfirmed", "IkinciAdi", "IkinciSoyadi", "IseGirisTarihi", "IstenCikisTarihi", "LockoutEnabled", "LockoutEnd", "Maas", "Meslek", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhoto", "SecurityStamp", "SirketId", "SirketId1", "Soyadi", "Status", "TC", "Token", "TwoFactorEnabled", "UpdatedTime", "UserName", "YoneticiId" },
                values: new object[] { "e1fd9964-2c65-486e-83c5-4743fe5a819d", 0, "Admin", "Ilgın Mahallesi, Hivda Sokak, No:1, Ankara", 1, "81ca39bd-d8be-4ed0-a31d-961e6743beb8", new DateTime(2024, 8, 15, 9, 45, 57, 209, DateTimeKind.Local).AddTicks(113), null, 0, new DateOnly(1997, 1, 1), 5, "admin@admin.com", true, null, null, new DateOnly(2024, 1, 1), new DateOnly(1, 1, 1), false, null, 10000m, 0, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEFEVFVgxSF+KOQEIhuQZWE6Lq8Eso7TjHuQ3Px3AcUCFD+TPoynUdEuOXmkIO5sp8Q==", null, false, "https://randomuser.me/api/portraits/men/11.jpg", "8678980EED564CECB09BD68613AC7382", new Guid("7d06417e-6ccf-4680-4462-08dcb8609bcc"), null, "User", 3, "12345678901", null, false, null, "admin", null });
        }
    }
}