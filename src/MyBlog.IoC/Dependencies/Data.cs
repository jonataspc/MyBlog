using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlog.Infra.Data.Context;

namespace MyBlog.IoC.Dependencies
{
    internal static class Data
    {
        public static void RegisterDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyBlogDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<MyBlogDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .UseExceptionProcessor();
                options.UseLazyLoadingProxies();

#if DEBUG
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
#endif
            });

#if DEBUG
            services.AddDatabaseDeveloperPageExceptionFilter();
#endif
        }
    }
}