using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.IoC.Dependencies;

namespace MyBlog.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataDependencies(configuration);
            services.RegisterRepositoryDependencies(configuration);
            services.RegisterServiceDependencies(configuration);
            services.RegisterIdentityDependencies(configuration);
        }
    }
}