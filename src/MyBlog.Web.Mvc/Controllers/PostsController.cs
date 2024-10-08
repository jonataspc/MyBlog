﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Exceptions;
using MyBlog.Domain.Services;
using MyBlog.Web.Mvc.Controllers.Base;
using MyBlog.Web.Mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Mvc.Controllers
{
    [Route("posts")]
    public class PostsController(
        IAppIdentityUser appIdentityUser,
        IPostService postService,
        IAuthorService authorService,
        IConfiguration configuration,
        ILogger<PostsController> logger) : AppControllerBase
    {
        [Route("{pageNumber:int?}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index([Range(1, int.MaxValue)] int? pageNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int pageSize = configuration.GetValue<int>("Posts:PageSize");
            return View(await postService.GetAvailablePostsPaginatedAsync(pageNumber ?? 1, pageSize));
        }

        [Route("authors/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Authors(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var author = await authorService.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            ViewData["Message"] = $"Você está visualizando posts do autor <strong>{author.User.FullName}</strong>.";
            ViewData["MessageLink"] = "Visualizar posts de todos os autores";
            return View(nameof(Index), await postService.GetPostsByAuthorAsync(id));
        }

        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term) || !ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var posts = await postService.SearchByTermAsync(term);

            ViewData["Message"] = $"Existem <strong>{posts.Count()}</strong> post(s) que corresponde(m) à sua pesquisa.";
            ViewData["MessageLink"] = "Visualizar todos os posts";
            return View(nameof(Index), posts);
        }

        [Route("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> View(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var post = await postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            await postService.IncrementViewsAsync(id);

            return View(post);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            var model = new PostViewModel();
            return View(model);
        }

        [HttpPost("novo")]
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

        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var post = await postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            if (!post.AllowEditOrDelete(appIdentityUser))
            {
                return Forbid();
            }

            return View(post.Adapt<PostViewModel>());
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Summary,Content,PublishDate,Id")] PostViewModel post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await postService.UpdateAsync(post.Adapt<Post>());
                }
                catch (NotAllowedOperationException e)
                {
                    logger.LogError(e, e.Message);
                    return Forbid();
                }

                return RedirectToAction(nameof(View), new { id });
            }

            return View(post);
        }

        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var post = await postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            if (!post.AllowEditOrDelete(appIdentityUser))
            {
                return Forbid();
            }

            return View(nameof(Delete), post);
        }

        [HttpPost("excluir/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await postService.RemoveAsync(id);
                }
                catch (NotAllowedOperationException e)
                {
                    logger.LogError(e, e.Message);
                    return Forbid();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}