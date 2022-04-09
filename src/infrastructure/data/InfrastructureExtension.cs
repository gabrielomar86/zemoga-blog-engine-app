using BlogEngineApp.core.interfaces.contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngineApp.infrastructure.data
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfraestructura(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddDbContext<BlogEngineAppContext>(
                x => x.UseSqlite(connectionString)
            );

            return services;
        }

    }
}
