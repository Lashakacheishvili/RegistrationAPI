using Registration.AuthConfig;
using Microsoft.Extensions.DependencyInjection;
using FriendsApi.OAuth;

namespace RegistrationAPI.Injection
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection IdentityServerInjection(this IServiceCollection services, string apiHost)
        {
            services.AddIdentityServer(a =>
            {
                a.IssuerUri = apiHost;
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryPersistedGrants()
            .AddInMemoryApiResources(ClientConfiguration.GetApiResources())
            .AddInMemoryClients(ClientConfiguration.GetClients())
            .AddInMemoryApiScopes(ClientConfiguration.GetApiScopes())
            .AddResourceOwnerValidator<AppResourceOwnerPasswordValidator>();
            return services;
        }
    }
}