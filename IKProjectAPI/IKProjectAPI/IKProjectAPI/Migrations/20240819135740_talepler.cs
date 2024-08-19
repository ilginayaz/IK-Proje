using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class talepler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_izinIstekleri_AspNetUsers_ApplicationUserId",
                table: "izinIstekleri");

            migrationBuilder.DropForeignKey(
                name: "FK_izinIstekleri_izinTipleri_IzinTipiId",
                table: "izinIstekleri");

            migrationBuilder.DropTable(
                name: "izinOdenekler");

            migrationBuilder.DropTable(
                name: "masraflar");

            migrationBuilder.DropTable(
                name: "izinTipleri");

            migrationBuilder.DropTable(
                name: "masrafTipleri");

            migrationBuilder.DropIndex(
                name: "IX_izinIstekleri_IzinTipiId",
                table: "izinIstekleri");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "izinIstekleri");

            migrationBuilder.RenameColumn(
                name: "IzinTipiId",
                table: "izinIstekleri",
                newName: "IzinTuru");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "izinIstekleri",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AvansTalepleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TalepTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParaBirimi = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvansTalepleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvansTalepleri_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HarcamaTalepleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiderTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParaBirimi = table.Column<int>(type: "int", nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    MasrafTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarcamaTalepleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarcamaTalepleri_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "d8bd58c5-6a42-4a6e-a0b3-1413a22472bf", new DateTime(2024, 8, 19, 16, 57, 39, 838, DateTimeKind.Local).AddTicks(9216), "AQAAAAIAAYagAAAAEBamkcoUxVV2E2344xHtJ4DvW+bd5Ym//FBj6jWlj68AYiISkkdRpdPZc0opiYMlqQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AvansTalepleri_ApplicationUserId",
                table: "AvansTalepleri",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarcamaTalepleri_ApplicationUserId",
                table: "HarcamaTalepleri",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_izinIstekleri_AspNetUsers_ApplicationUserId",
                table: "izinIstekleri",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_izinIstekleri_AspNetUsers_ApplicationUserId",
                table: "izinIstekleri");

            migrationBuilder.DropTable(
                name: "AvansTalepleri");

            migrationBuilder.DropTable(
                name: "HarcamaTalepleri");

            migrationBuilder.RenameColumn(
                name: "IzinTuru",
                table: "izinIstekleri",
                newName: "IzinTipiId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "izinIstekleri",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "izinIstekleri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "izinTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SirketId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultDays = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IzinTipiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izinTipleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_izinTipleri_sirketler_SirketId",
                        column: x => x.SirketId,
                        principalTable: "sirketler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "masrafTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SirketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasrafTipiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masrafTipleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_masrafTipleri_sirketler_SirketId",
                        column: x => x.SirketId,
                        principalTable: "sirketler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "izinOdenekler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IzinTipiId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GunSayisi = table.Column<int>(type: "int", nullable: false),
                    Periyot = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izinOdenekler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_izinOdenekler_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_izinOdenekler_izinTipleri_IzinTipiId",
                        column: x => x.IzinTipiId,
                        principalTable: "izinTipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "masraflar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MasrafTipiId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GiderTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasrafTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masraflar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_masraflar_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_masraflar_masrafTipleri_MasrafTipiId",
                        column: x => x.MasrafTipiId,
                        principalTable: "masrafTipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1fd9964-2c65-486e-83c5-4743fe5a819c",
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "PasswordHash" },
                values: new object[] { "d8839946-5369-4f58-acfb-38dac70a3102", new DateTime(2024, 8, 15, 12, 8, 7, 591, DateTimeKind.Local).AddTicks(5158), "AQAAAAIAAYagAAAAEEf9YbckX4peHf2upSrAS5JcJHr+MinPF+qloPXbQnMpHb/53yQGVi/ct1Kv6SAxKA==" });

            migrationBuilder.CreateIndex(
                name: "IX_izinIstekleri_IzinTipiId",
                table: "izinIstekleri",
                column: "IzinTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_izinOdenekler_ApplicationUserId",
                table: "izinOdenekler",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_izinOdenekler_IzinTipiId",
                table: "izinOdenekler",
                column: "IzinTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_izinTipleri_SirketId",
                table: "izinTipleri",
                column: "SirketId");

            migrationBuilder.CreateIndex(
                name: "IX_masraflar_ApplicationUserId",
                table: "masraflar",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_masraflar_MasrafTipiId",
                table: "masraflar",
                column: "MasrafTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_masrafTipleri_SirketId",
                table: "masrafTipleri",
                column: "SirketId");

            migrationBuilder.AddForeignKey(
                name: "FK_izinIstekleri_AspNetUsers_ApplicationUserId",
                table: "izinIstekleri",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_izinIstekleri_izinTipleri_IzinTipiId",
                table: "izinIstekleri",
                column: "IzinTipiId",
                principalTable: "izinTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
