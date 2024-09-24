using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Infra.Data.Context;
using MyBlog.Infra.Identity;

namespace MyBlog.IoC.Dependencies
{
    internal static class Identity
    {
        public static void RegisterIdentityDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
                    {
                        options.User.RequireUniqueEmail = true;
                        options.SignIn.RequireConfirmedAccount = false;
                    })
                .AddRoles<IdentityRole>()
                .AddUserManager<UserManagerExtended>()
                .AddEntityFrameworkStores<MyBlogDbContext>();
        }
    }
}