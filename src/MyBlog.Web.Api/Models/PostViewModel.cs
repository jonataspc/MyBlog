namespace MyBlog.Web.Api.Models
{
    public record PostViewModel(
        Guid Id,
        string Title, 
        string Summary, 
        string Content, 
        long ViewCount, 
        DateTime PublishDate, 
        string AuthorName,
        int CommentsCount);
}