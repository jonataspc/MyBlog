using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Domain.Data.Repositories;
using MyBlog.Domain.Data.Repositories.Base;
using MyBlog.Domain.Entities;
using MyBlog.Infra.Data.Context;
using MyBlog.Infra.Data.Repositories;
using MyBlog.Infra.Data.Repositories.Base;

namespace MyBlog.IoC.Dependencies
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