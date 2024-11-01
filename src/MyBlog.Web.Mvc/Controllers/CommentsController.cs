using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Entities;
using MyBlog.Core.Services.Interfaces;
using MyBlog.Web.Mvc.Controllers.Base;
using MyBlog.Web.Mvc.Models;

namespace MyBlog.Web.Mvc.Controllers
{
    [Route("comentarios")]
    public class CommentsController(
        IAppIdentityUser appIdentityUser,
        ICommentService commentService) : AppControllerBase
    {
        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([Bind("Content,PostId")] CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var c = new Comment
                {
                    Content = comment.Content,
                    IsActive = true,
                    PostId = comment.PostId,
                    Post = null!,
                    UserId = appIdentityUser.GetUserId(),
                    User = null!,
                };

                await commentService.AddAsync(c);
            }

            return RedirectToAction("View", "Posts", new { id = comment.PostId });
        }

        [Route("editar/{postId:guid}/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, Guid postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = await commentService.GetByIdAsync(id);

            if (comment == null || comment.PostId != postId)
            {
                return NotFound();
            }

            if (!comment.AllowEditOrDelete(appIdentityUser))
            {
                return Forbid();
            }

            return View(comment.Adapt<CommentViewModel>());
        }

        [HttpPost("editar/{postId:guid}/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Guid postId, [Bind("Content,Id")] CommentViewModel comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingComment = await commentService.GetByIdAsync(id);
                if (existingComment is null || existingComment.PostId != postId)
                {
                    return NotFound();
                }

                if (!existingComment.AllowEditOrDelete(appIdentityUser))
                {
                    return Forbid();
                }

                await commentService.UpdateAsync(comment.Adapt<Comment>());

                return RedirectToAction(nameof(View), "Posts", new { id = postId });
            }

            return View(comment);
        }

        [Route("excluir/{postId:guid}/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, Guid postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = await commentService.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            if (!comment.AllowEditOrDelete(appIdentityUser))
            {
                return Forbid();
            }

            ViewBag.PostId = postId;
            return View(comment);
        }

        [HttpPost("excluir/{postId:guid}/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid postId)
        {
            if (ModelState.IsValid)
            {
                var existingComment = await commentService.GetByIdAsync(id);
                if (existingComment is null || existingComment.PostId != postId)
                {
                    return NotFound();
                }

                if (!existingComment.AllowEditOrDelete(appIdentityUser))
                {
                    return Forbid();
                }

                await commentService.RemoveAsync(id);

                return RedirectToAction(nameof(View), "Posts", new { id = postId });
            }
            return RedirectToAction("Index", "Posts");
        }
    }
}