namespace MyBlog.Web.Api.Models
{
    public record AuthenticationResponseDto(string Token, WebApiUser WebApiUser);
}