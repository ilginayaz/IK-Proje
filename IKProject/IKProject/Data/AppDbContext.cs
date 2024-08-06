using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var hasher = new PasswordHasher<IdentityUser<int>>();


            var adminRoleId = 1;
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );


            var adminUserId = 1;
            modelBuilder.Entity<IdentityUser<int>>().HasData(
                new IdentityUser<int>
                {
                    Id = adminUserId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Aa123..."),
                    SecurityStamp = "{8678980E-ED56-4CEC-B09B-D68613AC7382}"
                }
            );
        }
    }
}