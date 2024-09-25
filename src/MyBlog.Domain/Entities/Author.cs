using MyBlog.Domain.Entities.Common;

namespace MyBlog.Domain.Entities
{
    public class Author : EntityBase
    {
        public required Guid UserId { get; set; }

        public virtual required User User { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = [];
    }
}