using MyBlog.Domain.Data.Repositories.Base;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;

namespace MyBlog.Application.Services
{
    public class AuthorService(IRepository<Author> repository) : IAuthorService
    {
        public async Task AddAsync(Author author)
        {
            repository.Insert(author);
            await repository.UnitOfWork.CommitAsync();
        }

        public async Task<Author?> GetByUserIdAsync(Guid userId)
        {
            return await repository.FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}