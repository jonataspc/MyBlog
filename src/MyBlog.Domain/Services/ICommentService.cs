using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Services
{
    public interface ICommentService
    {
        bool AllowDelete(Guid ownerUserId);

        Task RemoveAsync(Guid id);

        Task<Comment?> GetByIdAsync(Guid id);

        Task AddAsync(Comment comment);
    }
}