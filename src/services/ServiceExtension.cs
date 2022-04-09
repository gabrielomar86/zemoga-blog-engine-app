using BlogEngineApp.core.interfaces.contratos.servicios;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineApp.services
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogServicio>();

            return services;
        }
    }
}
