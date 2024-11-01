using MyBlog.Core.Data.Interfaces;
using MyBlog.Core.Entities;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.Services
{
    public class UserService(IRepository<User> repository, IAuthorService authorService) : IUserService
    {
        public async Task AddAsync(User user)
        {
            repository.Insert(user);
            await repository.UnitOfWork.CommitAsync();

            // Create author as well
            var author = new Author
            {
                UserId = user.Id,
                User = null!
            };

            await authorService.AddAsync(author);
        }
    }
}