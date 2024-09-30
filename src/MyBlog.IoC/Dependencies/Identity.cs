using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Domain.Services;
using MyBlog.Infra.Data.Context;
using MyBlog.Infra.Identity.Models;
using MyBlog.Infra.Identity.Services;

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

                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;
                    })
                .AddRoles<IdentityRole>()
                .AddUserManager<UserManagerExtended>()
                .AddEntityFrameworkStores<MyBlogDbContext>();


            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
        }
    }
}