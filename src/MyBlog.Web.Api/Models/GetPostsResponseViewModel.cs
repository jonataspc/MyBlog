namespace MyBlog.Web.Api.Models
{
    public record GetPostsResponseViewModel(int PageIndex, int TotalPages, bool HasPreviousPage, bool HasNextPage, IEnumerable<PostResponseViewModel> Posts);
}