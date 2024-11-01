using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Core.IoC.Dependencies;

namespace MyBlog.Core.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services.RegisterDataDependencies(configuration, hostEnvironment );
            services.RegisterRepositoryDependencies(configuration);
            services.RegisterServiceDependencies(configuration);
            services.RegisterIdentityDependencies(configuration);
        }
    }
}