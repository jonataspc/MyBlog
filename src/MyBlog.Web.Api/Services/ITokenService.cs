using MyBlog.Web.Api.Models;

namespace MyBlog.Web.Api.Services
{
    public interface ITokenService
    {
        string CreateToken(WebApiUser user);
    }
}