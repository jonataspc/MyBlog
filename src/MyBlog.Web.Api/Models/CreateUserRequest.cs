using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Api.Models
{
    public record CreateUserRequest(
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        string Email,

        [Required]
        string Password,

        [Required]
        string FullName);
}