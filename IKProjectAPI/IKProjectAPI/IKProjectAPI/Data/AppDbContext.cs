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
            //Seeding a  'Administrator' role to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2", Name = "Yönetici", NormalizedName = "YONETICI".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3", Name = "Çalışan", NormalizedName = "CALISAN".ToUpper() });
        }

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
