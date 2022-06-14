using Microsoft.Extensions.DependencyInjection;
using Service.ServiceImplementations;
using Service.ServiceInterfaces;

namespace Service.Injection
{
    public static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReasonService, ReasonService>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }
    }
}
