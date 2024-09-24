using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Services
{
    public interface IPostService
    {
        Task AddAsync(Post post, Guid userId);

        Task<IEnumerable<Post>> GetAvailablePostsAsync();

        bool AllowEditOrDelete(Guid ownerUserId);

        Task<Post?> GetByIdAsync(Guid id);

        Task UpdateAsync(Post post);

        Task RemoveAsync(Guid id, Guid userId);
        Task IncrementViewsAsync(Guid value);
    }
}