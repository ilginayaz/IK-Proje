using IKProjectAPI.Data.Concrete;
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

            builder.Entity<IdentityUserRole<int>>().HasKey(ir => new { ir.UserId, ir.RoleId });


            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Yönetici", NormalizedName = "YONETICI" },
                new IdentityRole { Id = "3", Name = "Çalışan", NormalizedName = "CALISAN" }
            );

            var hasher = new PasswordHasher<IdentityUser<int>>();
            builder.Entity<IdentityUser<int>>().HasData(
                new IdentityUser<int>
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Aa123..."),
                    SecurityStamp = "{8678980E-ED56-4CEC-B09B-D68613AC7382}"
                }
            );


            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1
                }
            );

        }
        public DbSet<Sirket> sirketler { get; set; }
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
