using MyBlog.Core.Entities.Common;

namespace MyBlog.Core.Entities
{
    public class Author : EntityBase
    {
        public required Guid UserId { get; set; }

        public virtual required User User { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = [];
    }
}