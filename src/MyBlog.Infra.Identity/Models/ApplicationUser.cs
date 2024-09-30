using Microsoft.AspNetCore.Identity;

namespace MyBlog.Infra.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required virtual string FullName { get; set; }
    }
}