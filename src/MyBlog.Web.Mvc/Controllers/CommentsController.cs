using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Exceptions;
using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Controllers.Base;
using MyBlog.Web.Mvc.Models;

namespace MyBlog.Web.Mvc.Controllers
{
    [Route("comentarios")]
    public class CommentsController(
        IAppIdentityUser appIdentityUser,
        ICommentService commentService,
        ILogger<CommentsController> logger) : AppControllerBase
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
                try
                {
                    await commentService.UpdateAsync(comment.Adapt<Comment>());
                }
                catch (NotAllowedOperationException e)
                {
                    logger.LogError(e, e.Message);
                    return Forbid();
                }

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
                try
                {
                    await commentService.RemoveAsync(id);
                }
                catch (NotAllowedOperationException e)
                {
                    logger.LogError(e, e.Message);
                    return Forbid();
                }

                return RedirectToAction(nameof(View), "Posts", new { id = postId });
            }
            return RedirectToAction("Index", "Posts");
        }
    }
}