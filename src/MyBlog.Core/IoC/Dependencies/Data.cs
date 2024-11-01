using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Core.Data.Context;

namespace MyBlog.Core.IoC.Dependencies
{
    internal static class Data
    {
        public static void RegisterDataDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            var connectionString = configuration.GetConnectionString("MyBlogDbConnection") ?? throw new InvalidOperationException("Connection string 'MyBlogDbConnection' not found.");
            services.AddDbContext<MyBlogDbContext>(options =>
            {
                if (hostEnvironment.IsDevelopment())
                {
                    options.UseSqlite(connectionString)
                           .UseExceptionProcessor();
                }
                else
                {
                    options.UseSqlServer(connectionString)
                           .UseExceptionProcessor();
                }

                    
                options.UseLazyLoadingProxies();

                if (hostEnvironment.IsDevelopment())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            if (hostEnvironment.IsDevelopment())
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }
        }
    }
}