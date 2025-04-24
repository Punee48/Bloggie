using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bloggie.Web;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions options) : base(options)
    {

    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminRoleId = "9ebeeb15-730e-4cec-9325-cd09c8022932";
        var superAdminRoleId = "eac6368d-c857-4469-8bee-2b702fbf02cf";
        var userRoleId = "a95dd2bd-af18-4fe1-858e-287eeb1b50da";
        //Creating a Roles to seed to the Database 
        var roles = new List<IdentityRole>{
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole
            {
                Name = "Super Admin",
                NormalizedName = "SUPER ADMIN",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId

            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId

            }

        };

        builder.Entity<IdentityRole>().HasData(roles);

        //Seed Super Admin User

        var superAdminId = "a596a046-3b48-4a96-8e0a-0a84bc7f3b91";

        var superAdminUser = new IdentityUser
        {
            UserName = "superadmin@bloggie.com",
            Email = "superadmin@bloggie.com",
            NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
            NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
            Id = superAdminId
        };

        //Creating Password for Super Admin
        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin");

        builder.Entity<IdentityUser>().HasData(superAdminUser);

        //Adding All roles to the Super Admin User

        var superAdminUserRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminId,
            },

            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId,
            },

            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = superAdminId,
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminUserRoles);


    }
}
