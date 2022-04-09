using Microsoft.Extensions.DependencyInjection;
namespace BlogEngineApp.core
{
    public static class CoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }

    }
}
