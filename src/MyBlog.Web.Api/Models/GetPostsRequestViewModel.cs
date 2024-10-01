using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Api.Models
{
    public record GetPostsRequestViewModel(
        [Range(1, int.MaxValue)]
        int? PageNumber);
}