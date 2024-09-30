using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.Mvc.Controllers.Base
{
    [Authorize]
    public abstract class AppControllerBase : Controller
    {
    }
}