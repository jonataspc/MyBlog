//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MyBlog.Domain.Entities;
//using MyBlog.Infra.Data.Context;

//namespace MyBlog.Web.Mvc.Controllers
//{
//    public class AuthorsController : Controller
//    {
//        private readonly MyBlogDbContext _context;

//        public AuthorsController(MyBlogDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Authors
//        public async Task<IActionResult> Index()
//        {
//            var myBlogDbContext = _context.Authors.Include(a => a.User);
//            return View(await myBlogDbContext.ToListAsync());
//        }

//        // GET: Authors/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var author = await _context.Authors
//                .Include(a => a.User)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (author == null)
//            {
//                return NotFound();
//            }

//            return View(author);
//        }

//        // GET: Authors/Create
//        public IActionResult Create()
//        {
//            ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName");
//            return View();
//        }

//        // POST: Authors/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Bio,Slug,UserId,Id,CreatedAt,ModifiedAt")] Author author)
//        {
//            if (ModelState.IsValid)
//            {
//                // author.Id = Guid.NewGuid();
//                _context.Add(author);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName", author.UserId);
//            return View(author);
//        }

//        // GET: Authors/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var author = await _context.Authors.FindAsync(id);
//            if (author == null)
//            {
//                return NotFound();
//            }
//            ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName", author.UserId);
//            return View(author);
//        }

//        // POST: Authors/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("Bio,Slug,UserId,Id,CreatedAt,ModifiedAt")] Author author)
//        {
//            if (id != author.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(author);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!AuthorExists(author.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["UserId"] = new SelectList(_context.BlogUsers, "Id", "FullName", author.UserId);
//            return View(author);
//        }

//        // GET: Authors/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var author = await _context.Authors
//                .Include(a => a.User)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (author == null)
//            {
//                return NotFound();
//            }

//            return View(author);
//        }

//        // POST: Authors/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var author = await _context.Authors.FindAsync(id);
//            if (author != null)
//            {
//                _context.Authors.Remove(author);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool AuthorExists(Guid id)
//        {
//            return _context.Authors.Any(e => e.Id == id);
//        }
//    }
//}
