using MyBlog.Core.Entities;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.Data.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAvailablePostsAsync();

        Task<IPaginatedList<Post>> GetAvailablePostsPaginatedAsync(int pageIndex, int pageSize);

        Task<IEnumerable<Post>> GetMostViewedPostsAsync(int numberOfPosts);

        Task<Post?> GetPostByIdWithRelatedEntitiesAsync(Guid id);

        Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid authorId);

        Task<IEnumerable<Post>> SearchByTermAsync(string term);
    }
}