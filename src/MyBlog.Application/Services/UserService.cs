using MyBlog.Domain.Data.Repositories.Base;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;

namespace MyBlog.Application.Services
{
    public class UserService(IRepository<User> repository) : IUserService
    {
        public async Task AddAsync(User user)
        {
            repository.Insert(user);
            await repository.UnitOfWork.CommitAsync();
        }
    }
}