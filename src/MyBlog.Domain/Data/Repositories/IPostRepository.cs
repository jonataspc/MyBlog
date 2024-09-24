using MyBlog.Domain.Data.Repositories.Base;
using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAvailablePostsAsync();
        Task<Post?> GetPostByIdWithRelatedEntitiesAsync(Guid id);
    }
}