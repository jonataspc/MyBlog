using MyBlog.Core.Data.Interfaces;
using MyBlog.Core.Entities;
using MyBlog.Core.Exceptions;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.Services
{
    public class PostService(IAppIdentityUser appIdentityUser, IPostRepository postRepository, IAuthorService authorService) : IPostService
    {
        public async Task AddAsync(Post post, Guid userId)
        {
            Author author = await authorService.GetByUserIdAsync(userId) ?? throw new BusinessException("Author not found for the given user");

            post.AuthorId = author.Id;
            postRepository.Insert(post);
            await postRepository.UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Post>> GetAvailablePostsAsync()
        {
            return await postRepository.GetAvailablePostsAsync();
        }

        public async Task<IPaginatedList<Post>> GetAvailablePostsPaginatedAsync(int pageIndex, int pageSize)
        {
            return await postRepository.GetAvailablePostsPaginatedAsync(pageIndex, pageSize);
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            return await postRepository.GetPostByIdWithRelatedEntitiesAsync(id);
        }

        public async Task<IEnumerable<Post>> GetMostViewedPostsAsync()
        {
            const int numberOfPosts = 7;
            return await postRepository.GetMostViewedPostsAsync(numberOfPosts);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid authorId)
        {
            return await postRepository.GetPostsByAuthorAsync(authorId);
        }

        public async Task IncrementViewsAsync(Guid value)
        {
            var post = await postRepository.GetAsync(value) ?? throw new ArgumentException("Post não existente");

            post.ViewCount++;
            postRepository.Update(post);
            await postRepository.UnitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var existingPost = await postRepository.GetAsync(id) ?? throw new ArgumentException("Post não existente");

            if (!existingPost.AllowEditOrDelete(appIdentityUser))
            {
                throw new NotAllowedOperationException("Usuário não autorizado");
            }

            existingPost.IsActive = false;
            postRepository.Update(existingPost);
            await postRepository.UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Post>> SearchByTermAsync(string term)
        {
            return await postRepository.SearchByTermAsync(term.Trim());
        }

        public async Task UpdateAsync(Post post)
        {
            var existingPost = await postRepository.GetAsync(post.Id) ?? throw new ArgumentException("Post não existente");

            if (!existingPost.AllowEditOrDelete(appIdentityUser))
            {
                throw new NotAllowedOperationException("Usuário não autorizado");
            }

            existingPost.Title = post.Title;
            existingPost.Summary = post.Summary;
            existingPost.Content = post.Content;
            existingPost.IsActive = post.IsActive;
            existingPost.PublishDate = post.PublishDate;

            postRepository.Update(existingPost);
            await postRepository.UnitOfWork.CommitAsync();
        }
    }
}