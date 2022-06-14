using IdentityServer4.Models;
using System.Collections.Generic;

namespace Registration.AuthConfig
{
    public class ClientConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource
            {
                Name = "Api",
                DisplayName = "Registration Api",
                Scopes = new[]
                {
                  "RegistrationApi",
                  "RegistrationUserApi"
                }
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Registration",
                    AllowedGrantTypes = GrantTypes.ClientCredentials ,
                    AllowOfflineAccess=true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new[] { "RegistrationApi" },
                    ClientSecrets = new[] { new Secret("5Aue2ks34fj".Sha256()) },
                    AccessTokenLifetime = 3600*24*365*10
                },
                 new Client
                {
                    ClientId = "RegistrationUser",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials ,
                    RequireClientSecret = false,
                    AllowOfflineAccess=true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new[] { "RegistrationUserApi" },
                    ClientSecrets = new[] { new Secret("5Aue2ks34fj".Sha256()) },
                    AccessTokenLifetime = 3600*24*365*10
                }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("RegistrationApi"),
                new ApiScope ("RegistrationUserApi")
            };
        }
    }
}
