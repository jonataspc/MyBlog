using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Api.Models
{
    public record CommentRequestViewModel(
        [Required]
        [MaxLength(1024)]
        string Content);
}