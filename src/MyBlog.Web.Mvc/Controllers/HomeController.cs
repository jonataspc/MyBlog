using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Mvc.Models;
using System.Diagnostics;

namespace MyBlog.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {

        // TODO: remover este controller e views...

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Posts");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
