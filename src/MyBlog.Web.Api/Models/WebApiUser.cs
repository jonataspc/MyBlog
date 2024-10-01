namespace MyBlog.Web.Api.Models
{
    public record WebApiUser(string Username, Guid UserId, IEnumerable<string> Roles);
}