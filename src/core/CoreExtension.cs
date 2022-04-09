using BlogEngineApp.core.extensiones;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineApp.core
{
    public static class CoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            //Enable AutoMapper
            services.AddAutoMapper(typeof(BlogEngineAppMappingProfile));

            return services;
        }

    }
}
