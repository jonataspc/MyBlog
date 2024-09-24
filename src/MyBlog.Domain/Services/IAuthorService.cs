using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Services
{
    public interface IAuthorService
    {
        Task AddAsync(Author author);

        Task<Author?> GetByUserIdAsync(Guid userId);

        Task<Author?> GetByIdAsync(Guid id);

        Task<IEnumerable<Author>?> GetLastAuthorsWithPostsAsync();
    }
}