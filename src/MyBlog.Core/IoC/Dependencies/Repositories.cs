using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Core.Data.Context;
using MyBlog.Core.Data.Interfaces;
using MyBlog.Core.Data.Repositories;
using MyBlog.Core.Data.Repositories.Base;
using MyBlog.Core.Entities;

namespace MyBlog.Core.IoC.Dependencies
{
    internal static class Repositories
    {
        public static void RegisterRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IRepository<Comment>, RepositoryBase<Comment, MyBlogDbContext>>();
            services.AddScoped<IRepository<Author>, RepositoryBase<Author, MyBlogDbContext>>();
            services.AddScoped<IRepository<User>, RepositoryBase<User, MyBlogDbContext>>();
        }
    }
}