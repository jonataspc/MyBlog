using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.Api.Controllers.Base
{
    [ApiController]
    [Authorize]
    public abstract class AppControleBase : ControllerBase
    {
    }
}