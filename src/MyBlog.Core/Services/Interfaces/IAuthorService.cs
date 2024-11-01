using MyBlog.Core.Entities;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IAuthorService
    {
        Task AddAsync(Author author);

        Task<Author?> GetByUserIdAsync(Guid userId);

        Task<Author?> GetByIdAsync(Guid id);

        Task<IEnumerable<Author>?> GetLastAuthorsWithPostsAsync();
    }
}