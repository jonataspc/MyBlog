using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Models;

namespace MyBlog.Web.Mvc.Controllers
{
    public class CommentsController(IAppIdentityUser appIdentityUser, ICommentService commentService) : Controller
    {
        //// GET: Comments
        //public async Task<IActionResult> Index()
        //{
        //    var myBlogDbContext = _context.Comments.Include(c => c.Post).Include(c => c.User);
        //    return View(await myBlogDbContext.ToListAsync());
        //}

        //// GET: Comments/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comments
        //        .Include(c => c.Post)
        //        .Include(c => c.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        //// GET: Comments/Create
        //public IActionResult Create()
        //{
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content");
        //    ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName");
        //    return View();
        //}

        //// POST: Comments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Content,IsActive,PostId,UserId,Id,CreatedAt,ModifiedAt")] Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // comment.Id = Guid.NewGuid();
        //        _context.Add(comment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
        //    ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName", comment.UserId);
        //    return View(comment);
        //}

        //// GET: Comments/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comments.FindAsync(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
        //    ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName", comment.UserId);
        //    return View(comment);
        //}

        //// POST: Comments/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Content,IsActive,PostId,UserId,Id,CreatedAt,ModifiedAt")] Comment comment)
        //{
        //    if (id != comment.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(comment);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CommentExists(comment.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
        //    ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName", comment.UserId);
        //    return View(comment);
        //}

        [HttpPost]
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

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return NotFound();
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

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                await commentService.RemoveAsync(id, appIdentityUser.GetUserId());
            }
            return RedirectToAction("Index", "Posts");
        }

        //private bool CommentExists(Guid id)
        //{
        //    return _context.Comments.Any(e => e.Id == id);
        //}
    }
}