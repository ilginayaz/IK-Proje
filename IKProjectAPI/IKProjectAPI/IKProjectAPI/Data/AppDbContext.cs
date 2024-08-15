using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IKProjectAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserRole<string>>().HasKey(ir => new { ir.UserId, ir.RoleId });


            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Yönetici", NormalizedName = "YONETICI" },
                new IdentityRole { Id = "3", Name = "Çalışan", NormalizedName = "CALISAN" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin1."),
                    SecurityStamp = "8678980EED564CECB09BD68613AC7382",



                    Id = Guid.Parse("{E1FD9964-2C65-486E-83C5-4743FE5A819C}").ToString(),
                    ProfilePhoto = "https://randomuser.me/api/portraits/men/11.jpg", 
                    Adi = "Admin",
                    Soyadi = "User",
                    DogumTarihi = new DateOnly(1997, 1, 1),
                    DogumYeri = Sehirler.ANKARA, 
                    TC = "12345678901", 
                    IseGirisTarihi = new DateOnly(2024, 1, 1),
                    SirketId = Guid.Parse("{7D06417E-6CCF-4680-4462-08DCB8609BCC}"), 
                    Meslek = Meslek.YazılımMühendisi, 
                    Departman = Departman.Yazılım, 
                    Adres = "Ilgın Mahallesi, Hivda Sokak, No:1, Ankara", 
                    Maas = 10000m, 
                    Cinsiyet = Cinsiyet.Kadın, 
                    Token = null 

                }
            );


            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("{E1FD9964-2C65-486E-83C5-4743FE5A819C}").ToString(),
                    RoleId = "1"
                }
            );

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Sirket) // ApplicationUser sınıfındaki Sirket özelliği
                .WithMany(s => s.SirketCalisanlari) // Sirket sınıfındaki SirketCalisanlari özelliği
                .HasForeignKey(u => u.SirketId) // ApplicationUser sınıfındaki SirketId özelliği
                .OnDelete(DeleteBehavior.Restrict); // İlişki silme davranışı

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Yonetici) // ApplicationUser sınıfındaki Yonetici özelliği
                .WithMany(u => u.Calisanlar) // Yonetici özelliğinin ApplicationUser sınıfındaki Calisanlar koleksiyonu
                .HasForeignKey(u => u.YoneticiId) // ApplicationUser sınıfındaki YoneticiId özelliği
                .OnDelete(DeleteBehavior.Restrict); // İlişki silme davranışını ayarlayın

        }
        public DbSet<IKProjectAPI.Data.Concrete.Sirket> sirketler { get; set; }
        public DbSet<Masraf> masraflar { get; set; }
        public DbSet<MasrafTipi> masrafTipleri { get; set; }
        public DbSet<IzinTipi> izinTipleri { get; set; }
        public DbSet<IzinOdenek> izinOdenekler { get; set; }
        public DbSet<IzinIstegi> izinIstekleri { get; set; }


        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "CALISAN", "YONETICI", "ADMIN" }; // Gerekli tüm rolleri buraya ekleyin
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
