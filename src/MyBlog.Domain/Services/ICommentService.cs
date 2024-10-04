using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Services
{
    public interface ICommentService
    {
        Task RemoveAsync(Guid id);

        Task<Comment?> GetByIdAsync(Guid id);

        Task AddAsync(Comment comment);

        Task UpdateAsync(Comment comment);
    }
}