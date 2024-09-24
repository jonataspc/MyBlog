using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Services
{
    public interface IUserService
    {
        Task AddAsync(User user);
    }
}