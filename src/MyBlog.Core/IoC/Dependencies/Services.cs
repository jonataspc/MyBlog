using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Core.Services;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.IoC.Dependencies
{
    internal static class Services
    {
        public static void RegisterServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICommentService, CommentService>();
        }
    }
}