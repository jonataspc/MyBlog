﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Data.Context;
using MyBlog.Core.Data.Interfaces;
using MyBlog.Core.Data.Repositories.Base;
using MyBlog.Core.Entities;
using MyBlog.Core.Services.Interfaces;
using System.Linq.Expressions;

namespace MyBlog.Core.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post, MyBlogDbContext>, IPostRepository
    {
        public PostRepository(MyBlogDbContext dbContext) : base(dbContext)
        {
        }

        private IQueryable<Post> GetBaseQuery(Expression<Func<Post, bool>>? predicate = default)
        {
            var query = _dbContext.Posts
                            .Include(p => p.Author)
                            .ThenInclude(c => c.User)
                            .Where(p => p.PublishDate <= DateTime.Now);

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return query.OrderByDescending(p => p.PublishDate)
                        .ThenBy(p => p.Id)
                        .AsNoTracking();
        }

        public async Task<IEnumerable<Post>> GetAvailablePostsAsync()
        {
            return await GetBaseQuery().ToListAsync();
        }

        public async Task<Post?> GetPostByIdWithRelatedEntitiesAsync(Guid id)
        {
            return await _dbContext.Posts
                .Include(p => p.Comments.Where(c => c.IsActive).OrderByDescending(c => c.CreatedAt).ThenBy(c => c.Id))
                .ThenInclude(a => a.User)
                .Include(p => p.Author)
                .ThenInclude(a => a.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid authorId)
        {
            return await GetBaseQuery(p => p.AuthorId == authorId)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchByTermAsync(string term)
        {
            // TODO: Implement full-text search
            return await GetBaseQuery(p => p.Content.Contains(term) || p.Title.Contains(term) || p.Summary.Contains(term))
                        .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetMostViewedPostsAsync(int numberOfPosts)
        {
            return await _dbContext.Posts
                           .Include(p => p.Author)
                           .ThenInclude(c => c.User)
                           .Where(p => p.PublishDate <= DateTime.Now)
                           .OrderByDescending(p => p.ViewCount)
                           .ThenBy(p => p.Id)
                           .Take(numberOfPosts)
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<IPaginatedList<Post>> GetAvailablePostsPaginatedAsync(int pageIndex, int pageSize)
        {
            return await PaginatedList<Post>.CreateAsync(GetBaseQuery(), pageIndex, pageSize);
        }
    }
}