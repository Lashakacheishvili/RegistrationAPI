using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RegistrationAPI.Injection
{
    public static class DbContextExtensions
    {
        public static IServiceCollection DbContextInjection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RegistrationContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<User, UserPermission>(o =>
            {
                o.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
            })
            .AddEntityFrameworkStores<RegistrationContext>()
            .AddDefaultTokenProviders();
            return services;
        }
    }
}
