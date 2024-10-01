using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Api.Models
{
    public record GetPostsRequestDto(
        [Range(1, int.MaxValue)]
        int? PageNumber);
}