using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class RegistrationContext : IdentityDbContext<User, UserPermission, int>
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reason>  Reasons { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            var login = builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            login.HasKey("LoginProvider", "ProviderKey");
            var token = builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            token.HasKey("UserId", "LoginProvider", "Name");
            var role = builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            role.HasKey("UserId", "RoleId");
            builder.Entity<UserPermission>().ToTable("UserPermission");

        }
    }
}
