using BlogEngineApp.core.extensions;
using BlogEngineApp.core.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineApp.services
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreationPostFlowHandler).Assembly);
            services.AddTransient<ICreationPostFlowNotifier, CreationPostFlowNotifier>();

            services.AddScoped<IPostService, PostService>();

            return services;
        }
    }
}
