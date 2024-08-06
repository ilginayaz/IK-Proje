using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktiflik",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Sirket",
                table: "AspNetUsers",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SirketId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoneticiId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sirketler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SirketAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SirketNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiOfisi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalisanSayisi = table.Column<int>(type: "int", nullable: false),
                    SirketEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sehir = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostaKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sirketler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "izinTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzinTipiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultDays = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SirketId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    MasrafTipiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SirketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masrafTipleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_masrafTipleri_sirketler_SirketId",
                        column: x => x.SirketId,
                        principalTable: "sirketler",
                        principalColumn: "Id"
                        );
                });

            migrationBuilder.CreateTable(
                name: "izinIstekleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IzinGunSayisi = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IstekYorumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IzinTipiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izinIstekleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_izinIstekleri_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_izinIstekleri_izinTipleri_IzinTipiId",
                        column: x => x.IzinTipiId,
                        principalTable: "izinTipleri",
                        principalColumn: "Id"
                        );
                });

            migrationBuilder.CreateTable(
                name: "izinOdenekler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunSayisi = table.Column<int>(type: "int", nullable: false),
                    Periyot = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IzinTipiId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izinOdenekler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_izinOdenekler_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id"
                        );
                    table.ForeignKey(
                        name: "FK_izinOdenekler_izinTipleri_IzinTipiId",
                        column: x => x.IzinTipiId,
                        principalTable: "izinTipleri",
                        principalColumn: "Id"
                        );
                });

            migrationBuilder.CreateTable(
                name: "masraflar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiderTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    MasrafTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MasrafTipiId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masraflar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_masraflar_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id"
                        );
                    table.ForeignKey(
                        name: "FK_masraflar_masrafTipleri_MasrafTipiId",
                        column: x => x.MasrafTipiId,
                        principalTable: "masrafTipleri",
                        principalColumn: "Id"
                        );
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SirketId",
                table: "AspNetUsers",
                column: "SirketId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_YoneticiId",
                table: "AspNetUsers",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_izinIstekleri_ApplicationUserId",
                table: "izinIstekleri",
                column: "ApplicationUserId");

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
                principalColumn: "Id"
                );
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

            migrationBuilder.DropTable(
                name: "izinIstekleri");

            migrationBuilder.DropTable(
                name: "izinOdenekler");

            migrationBuilder.DropTable(
                name: "masraflar");

            migrationBuilder.DropTable(
                name: "izinTipleri");

            migrationBuilder.DropTable(
                name: "masrafTipleri");

            migrationBuilder.DropTable(
                name: "sirketler");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SirketId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_YoneticiId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SirketId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YoneticiId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AspNetUsers",
                newName: "Sirket");

            migrationBuilder.AddColumn<int>(
                name: "Aktiflik",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
