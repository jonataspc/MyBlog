using MyBlog.Domain.Entities.Common;

namespace MyBlog.Domain.Entities
{
    public class Comment : EntityBase
    {
        public required string Content { get; set; }

        public virtual bool IsActive { get; set; } = true;

        public required Guid PostId { get; set; }

        public virtual required Post Post { get; set; }

        public required Guid UserId { get; set; }

        public virtual required User User { get; set; }
    }
}