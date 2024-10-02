namespace MyBlog.Web.Api.Models
{
    public record CommentResponseViewModel(
        Guid Id,
        string Content,
        string UserName,
        DateTime CreatedAt);
}