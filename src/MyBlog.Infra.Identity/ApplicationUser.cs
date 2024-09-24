using Microsoft.AspNetCore.Identity;

namespace MyBlog.Infra.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public required virtual string FullName { get; set; }
    }
}