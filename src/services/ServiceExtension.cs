using BlogEngineApp.core.extensions;
using BlogEngineApp.core.interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using services.middleware;

namespace BlogEngineApp.services
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreationPostFlowHandler).Assembly);
            services.AddTransient<ICreationPostFlowNotifier, CreationPostFlowNotifier>();

            //Enable AutoMapper
            services.AddAutoMapper(typeof(BlogEngineAppMappingProfile));

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
