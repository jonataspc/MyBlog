using MyBlog.Domain.Data.Repositories;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Exceptions;
using MyBlog.Domain.Services;

namespace MyBlog.Application.Services
{
    public class PostService(IAppIdentityUser appIdentityUser, IPostRepository postRepository, IAuthorService authorService) : IPostService
    {
        public async Task AddAsync(Post post, Guid userId)
        {
            // Create author if not exists
            Author? author = await authorService.GetByUserIdAsync(userId);

            if (author is null)
            {
                author = new Author
                {
                    UserId = userId,
                    User = null!
                };

                await authorService.AddAsync(author);
            }

            post.AuthorId = author.Id;
            postRepository.Insert(post);
            await postRepository.UnitOfWork.CommitAsync();
        }

        public bool AllowEditOrDelete(Guid ownerUserId)
        {
            return appIdentityUser.IsAdmin() || appIdentityUser.GetUserId() == ownerUserId;
        }

        public async Task<IEnumerable<Post>> GetAvailablePostsAsync()
        {
            return await postRepository.GetAvailablePostsAsync();
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            return await postRepository.GetPostByIdWithRelatedEntitiesAsync(id);
        }

        public async Task IncrementViewsAsync(Guid value)
        {
            var post = await postRepository.GetAsync(value) ?? throw new ArgumentException("Post não existente");

            post.ViewCount++;
            postRepository.Update(post);
            await postRepository.UnitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Guid id, Guid userId)
        {
            var existingPost = await postRepository.GetAsync(id) ?? throw new ArgumentException("Post não existente");

            if (!AllowEditOrDelete(existingPost.Author.UserId))
            {
                throw new BusinessException("Usuário não autorizado");
            }

            existingPost.IsActive = false;
            postRepository.Update(existingPost);
            await postRepository.UnitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            var existingPost = await postRepository.GetAsync(post.Id) ?? throw new ArgumentException("Post não existente");

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