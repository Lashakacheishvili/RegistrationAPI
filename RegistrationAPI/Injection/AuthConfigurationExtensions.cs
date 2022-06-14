﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace RegistrationAPI.Injection
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, string apiHost)
        {
            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters.RequireExpirationTime = false;
                options.Authority = apiHost;
                options.RequireHttpsMetadata = false;
                options.Audience = "Api";
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    UseProxy = false
                };
            });
            services.AddAuthorization(c =>
            {
                c.AddPolicy("Registration", p =>
                {
                    p.RequireClaim("scope", new string[]
                    {
                        "RegistrationApi"
                    });
                });
            });
            return services;
        }
        public static IServiceCollection AddConfigure(this IServiceCollection services)
        {
            services.Configure(delegate (RequestLocalizationOptions options)
            {
                var supportedCultures = new List<CultureInfo>
                {
                 new CultureInfo("ka"),
                 new CultureInfo("en")
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                foreach (var item in supportedCultures)
                {
                    item.NumberFormat.NumberDecimalSeparator = ".";
                }
            });
            return services;
        }
    }
}
