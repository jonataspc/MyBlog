using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Mvc.Controllers.Base;
using MyBlog.Web.Mvc.Models;
using System.Diagnostics;

namespace MyBlog.Web.Mvc.Controllers
{
    [Route("")]
    [AllowAnonymous]
    public class HomeController : AppControllerBase
    {
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Posts");
        }

        [Route("politica-de-privacidade")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}