using MyBlog.Domain.Data.Repositories.Base;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Exceptions;
using MyBlog.Domain.Services;

namespace MyBlog.Application.Services
{
    public class CommentService(IAppIdentityUser appIdentityUser, IRepository<Comment> commentRepository) : ICommentService
    {
        public async Task AddAsync(Comment comment)
        {
            commentRepository.Insert(comment);
            await commentRepository.UnitOfWork.CommitAsync();
        }

        public bool AllowDelete(Guid ownerUserId)
        {
            return appIdentityUser.IsAdmin() || appIdentityUser.GetUserId() == ownerUserId;
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await commentRepository.GetAsync(id);
        }

        public async Task RemoveAsync(Guid id, Guid userId)
        {
            var comment = await commentRepository.GetAsync(id) ?? throw new ArgumentException("Comentário não existente");

            if (!AllowDelete(comment.Post.Author.UserId))
            {
                throw new BusinessException("Usuário não autorizado");
            }

            comment.IsActive = false;
            commentRepository.Update(comment);
            await commentRepository.UnitOfWork.CommitAsync();
        }
    }
}