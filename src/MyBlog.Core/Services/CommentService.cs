﻿using MyBlog.Core.Data.Interfaces;
using MyBlog.Core.Entities;
using MyBlog.Core.Exceptions;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.Services
{
    public class CommentService(IAppIdentityUser appIdentityUser, IRepository<Comment> commentRepository) : ICommentService
    {
        public async Task AddAsync(Comment comment)
        {
            commentRepository.Insert(comment);
            await commentRepository.UnitOfWork.CommitAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await commentRepository.GetAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var comment = await commentRepository.GetAsync(id) ?? throw new ArgumentException("Comentário não existente");

            if (!comment.AllowEditOrDelete(appIdentityUser))
            {
                throw new NotAllowedOperationException("Usuário não autorizado");
            }

            comment.IsActive = false;
            commentRepository.Update(comment);
            await commentRepository.UnitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            var existingComment = await commentRepository.GetAsync(comment.Id) ?? throw new ArgumentException("Comentário não existente");

            if (!existingComment.AllowEditOrDelete(appIdentityUser))
            {
                throw new NotAllowedOperationException("Usuário não autorizado");
            }

            existingComment.Content = comment.Content;
            commentRepository.Update(existingComment);
            await commentRepository.UnitOfWork.CommitAsync();
        }
    }
}