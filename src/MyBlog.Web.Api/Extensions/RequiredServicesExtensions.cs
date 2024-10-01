using Mapster;
using MyBlog.Web.Api.Services;
using System.Reflection;

namespace MyBlog.Web.Api.Extensions
{
    public static class RequiredServicesExtensions
    {
        public static void RegisterWebApiServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IWebApiUserService, WebApiUserService>();

            // Mapster mappings
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}