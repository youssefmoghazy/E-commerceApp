using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StackExchange.Redis;

namespace Persistance.Identity;

public class StoreIdentityDBContext (DbContextOptions<StoreIdentityDBContext> options)
    : IdentityDbContext<ApplicationUser,IdentityRole,string>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Address>().ToTable(nameof(Address));
        
        builder.Entity<IdentityUser>().ToTable(nameof(IdentityUser));
        builder.Entity<IdentityRole>().ToTable(nameof(IdentityRole));
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserToken<string>>();
    }
}
/// DbContext => 0 DBSet
/// IdentityDBContext => 7 DBSet {User [UI], Role [RI], UserRole, 
///     RoleClaims, UserClaims, UserLogins, UserTokens }
///     