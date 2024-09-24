using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Services;

namespace MyBlog.Web.Mvc.ViewComponents
{
    public class LastAuthorsWidgetViewComponent(IAuthorService authorService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await authorService.GetLastAuthorsWithPostsAsync();
            return View(posts);
        }
    }
}