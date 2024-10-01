using MyBlog.Web.Api.Models;

namespace MyBlog.Web.Api.Services
{
    public interface IWebApiUserService
    {
        Task<WebApiUser?> AuthenticateAsync(string? username, string? password);
    }
}