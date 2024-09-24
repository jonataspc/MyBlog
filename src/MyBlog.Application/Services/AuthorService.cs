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

        public async Task<Author?> GetByIdAsync(Guid id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<Author?> GetByUserIdAsync(Guid userId)
        {
            return await repository.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<IEnumerable<Author>?> GetLastAuthorsWithPostsAsync()
        {
            return await repository.GetAsync(predicate: a => a.Posts.Any(),
                                             skip: 0,
                                             take: 7,
                                             orderBy: null,
                                             orderByDescending: a => a.CreatedAt);
        }
    }
}