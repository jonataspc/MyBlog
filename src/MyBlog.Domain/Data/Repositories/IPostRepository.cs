using MyBlog.Domain.Data.Repositories.Base;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;

namespace MyBlog.Domain.Data.Repositories
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