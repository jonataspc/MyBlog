using Microsoft.AspNetCore.Identity;

namespace MyBlog.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required virtual string FullName { get; set; }
    }
}