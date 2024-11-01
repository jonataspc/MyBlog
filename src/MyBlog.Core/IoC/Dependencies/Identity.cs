using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Core.Data.Context;
using MyBlog.Core.Models;
using MyBlog.Core.Services.Identity;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.IoC.Dependencies
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