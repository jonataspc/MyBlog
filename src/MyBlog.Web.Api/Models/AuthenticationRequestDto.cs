using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Api.Models
{
    public record AuthenticationRequestDto(
        [Required]
        string Password,

        [Required]
        string Username);
}