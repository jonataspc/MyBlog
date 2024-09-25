using MyBlog.Domain.Entities.Common;

namespace MyBlog.Domain.Entities
{
    public class User : EntityBase
    {
        public required string FullName { get; set; }
    }
}