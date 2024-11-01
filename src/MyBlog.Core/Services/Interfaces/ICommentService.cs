using MyBlog.Core.Entities;

namespace MyBlog.Core.Services.Interfaces
{
    public interface ICommentService
    {
        Task RemoveAsync(Guid id);

        Task<Comment?> GetByIdAsync(Guid id);

        Task AddAsync(Comment comment);

        Task UpdateAsync(Comment comment);
    }
}