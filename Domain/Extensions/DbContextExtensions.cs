using Common.Enums;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Domain.Extensions
{
    public static class DbContextExtensions
    {
        public static void Seed(RegistrationContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            if (!context.Roles.Any())
            {
                var Roles = Enum.GetValues(typeof(UserRole));

                var roleManager = serviceProvider.GetRequiredService<RoleManager<UserPermission>>();
                foreach (UserRole r in Roles)
                {
                    var role = r.ToString();
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new UserPermission { Name = role, NormalizedName = role.ToUpper() }).Wait();
                    }
                }
            }
        }
    }
}
