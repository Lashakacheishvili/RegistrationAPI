using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RegistrationAPI.Injection;
using Service.Injection;
using System;

namespace RegistrationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            string apiHost = Configuration.GetValue<string>("APIHost").TrimEnd('/') + "/";
            services.DbContextInjection(Configuration.GetConnectionString("DefaultConnection"));
            services.IdentityServerInjection(apiHost);
            services.AddAuthConfiguration(apiHost);
            services.AddControllers();
            services.AddSwaggerDocumentation(apiHost);
            services.AddService();
            services.AddConfigure();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RegistrationContext dbContext, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #region Use Swagger
            app.UseSwaggerDocumentation();
            #endregion
            Domain.Extensions.DbContextExtensions.Seed(dbContext, serviceProvider);
        }
    }
}
