using MyBlog.Core.Entities;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Add an user and create an author for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddAsync(User user);
    }
}