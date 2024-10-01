namespace MyBlog.Web.Api.Models
{
    public record CommentViewModel(
        Guid Id, 
        string Content, 
        string UserName,
        DateTime CreatedAt);
}