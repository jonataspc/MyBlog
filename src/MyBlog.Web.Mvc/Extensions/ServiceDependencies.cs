using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Services.Identity;

namespace MyBlog.Web.Mvc.Extensions
{
    internal static class ServiceDependencies
    {
        public static void RegisterLocalServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
        }
    }
}