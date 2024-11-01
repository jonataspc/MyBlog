using MyBlog.Core.Entities;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IPostService
    {
        Task AddAsync(Post post, Guid userId);

        Task<IEnumerable<Post>> GetAvailablePostsAsync();

        Task<IPaginatedList<Post>> GetAvailablePostsPaginatedAsync(int pageIndex, int pageSize);

        Task<Post?> GetByIdAsync(Guid id);

        Task UpdateAsync(Post post);

        Task RemoveAsync(Guid id);

        Task IncrementViewsAsync(Guid value);

        Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid authorId);

        Task<IEnumerable<Post>> SearchByTermAsync(string term);

        Task<IEnumerable<Post>> GetMostViewedPostsAsync();
    }
}