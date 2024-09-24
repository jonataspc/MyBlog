using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Data.Repositories;
using MyBlog.Domain.Entities;
using MyBlog.Infra.Data.Context;
using MyBlog.Infra.Data.Repositories.Base;

namespace MyBlog.Infra.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post, MyBlogDbContext>, IPostRepository
    {
        public PostRepository(MyBlogDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetAvailablePostsAsync()
        {
            return await _dbContext.Posts
                .Include(p => p.Author)
                .ThenInclude(c => c.User)
                .Where(p => p.PublishDate <= DateTime.Now)
                .OrderByDescending(p => p.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Post?> GetPostByIdWithRelatedEntitiesAsync(Guid id)
        {
            return await _dbContext.Posts
                .Include(p => p.Comments.Where(c => c.IsActive))
                .ThenInclude(a => a.User)
                .Include(p => p.Author)
                .ThenInclude(a => a.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}