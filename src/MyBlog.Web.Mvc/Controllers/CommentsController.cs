using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Models;

namespace MyBlog.Web.Mvc.Controllers
{
    public class CommentsController(IAppIdentityUser appIdentityUser, ICommentService commentService) : Controller
    {
        [HttpPost]
        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Delete(Guid? id, Guid? postId)
        {
            if (id == null || postId == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = await commentService.GetByIdAsync(id.Value);

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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