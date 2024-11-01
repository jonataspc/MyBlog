using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Web.Mvc.ViewComponents
{
    public class LastAuthorsWidgetViewComponent(IAuthorService authorService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await authorService.GetLastAuthorsWithPostsAsync());
        }
    }
}