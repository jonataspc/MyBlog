using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Controllers.Base;
using MyBlog.Web.Mvc.Models;

namespace MyBlog.Web.Mvc.Controllers
{
    [Route("comentarios")]
    public class CommentsController(IAppIdentityUser appIdentityUser, ICommentService commentService) : AppControllerBase
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

            if (!commentService.AllowDelete(comment.Post.Author.UserId))
            {
                return Unauthorized();
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
                await commentService.RemoveAsync(id, appIdentityUser.GetUserId());
                return RedirectToAction(nameof(View), "Posts", new { id = postId });
            }
            return RedirectToAction("Index", "Posts");
        }
    }
}