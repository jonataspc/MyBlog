namespace MyBlog.Web.Api.Models
{
    public record GetPostsResponseDto(int PageIndex, int TotalPages, bool HasPreviousPage, bool HasNextPage, IEnumerable<PostViewModel> Posts);
}