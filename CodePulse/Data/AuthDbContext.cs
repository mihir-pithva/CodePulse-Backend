using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Data
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions): base(dbContextOptions)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //create reader and writer role

            string readerRoleId = "d4b2285f-7469-416e-b611-1611cb22ce41";
            string writerRoleId = "da7d41b0-61e5-4e85-b17b-c8033c12ccb2";


            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                },
            };

            //seeding data 
            builder.Entity<IdentityRole>().HasData(roles);

            //create admin user
            var adminUserId = "35440619 - 1436 - 4ed6 - a793 - 0b6d0d99b075";
            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@exp.com",
                Email = "admin@exp.com",
                NormalizedEmail = "admin@exp.com".ToUpper(),
                NormalizedUserName = "admin@exp.com".ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
