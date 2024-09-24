using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Services
{
    public interface ICommentService
    {
        bool AllowDelete(Guid ownerUserId);

        Task RemoveAsync(Guid id, Guid userId);

        Task<Comment?> GetByIdAsync(Guid id);

        Task AddAsync(Comment comment);
    }
}