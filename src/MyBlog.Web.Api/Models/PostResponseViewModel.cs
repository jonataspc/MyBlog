namespace MyBlog.Web.Api.Models
{
    public record PostResponseViewModel(
        Guid Id,
        string Title, 
        string Summary, 
        string Content, 
        long ViewCount, 
        DateTime PublishDate, 
        string AuthorName,
        int CommentsCount);
}