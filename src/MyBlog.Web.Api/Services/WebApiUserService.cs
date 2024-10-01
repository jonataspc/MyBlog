using Microsoft.AspNetCore.Identity;
using MyBlog.Infra.Identity.Models;
using MyBlog.Web.Api.Models;

namespace MyBlog.Web.Api.Services
{
    public class WebApiUserService(SignInManager<ApplicationUser> signInManager, ILogger<WebApiUserService> logger) : IWebApiUserService
    {
        public async Task<WebApiUser?> AuthenticateAsync(string? username, string? password)
        {
            ArgumentNullException.ThrowIfNull(username);
            ArgumentNullException.ThrowIfNull(password);

            var user = await signInManager.UserManager.FindByNameAsync(username);

            if (user is null)
            {
                logger.LogInformation("User not found: {Username}", username);
                return null;
            }

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                logger.LogInformation("Bad login: {Reason}, email: {Username}", signInResult.ToString(), username);
                return null;
            }

            return new WebApiUser(user.UserName!, Guid.Parse(user.Id));
        }
    }
}