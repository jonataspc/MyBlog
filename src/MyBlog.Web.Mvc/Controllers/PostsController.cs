using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Models;

namespace MyBlog.Web.Mvc.Controllers
{
    public class PostsController(IAppIdentityUser appIdentityUser, IPostService postService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await postService.GetAvailablePostsAsync());
        }

        //// GET: Posts/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts
        //        .Include(p => p.Author)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        public async Task<IActionResult> View(Guid? id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return NotFound();
            }

            var post = await postService.GetByIdAsync(id.Value);

            if (post == null)
            {
                return NotFound();
            }

            await postService.IncrementViewsAsync(id.Value);

            return View(post);
        }

        public IActionResult Create()
        {
            var model = new PostViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Summary,Content,PublishDate")] PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = postViewModel.Adapt<Post>();
                await postService.AddAsync(post, appIdentityUser.GetUserId());

                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return NotFound();
            }

            var post = await postService.GetByIdAsync(id.Value);

            if (post == null)
            {
                return NotFound();
            }

            if (!postService.AllowEditOrDelete(post.Author.UserId))
            {
                return Unauthorized();
            }
            ViewBag.Id = id;
            return View(post.Adapt<PostViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Summary,Content,PublishDate,Id")] PostViewModel post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await postService.UpdateAsync(post.Adapt<Post>());
                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return NotFound();
            }

            var post = await postService.GetByIdAsync(id.Value);

            if (post == null)
            {
                return NotFound();
            }

            if (!postService.AllowEditOrDelete(post.Author.UserId))
            {
                return Unauthorized();
            }

            return View(nameof(Delete), post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                await postService.RemoveAsync(id, appIdentityUser.GetUserId());
            }
            return RedirectToAction(nameof(Index));
        }
    }
}