using MyBlog.Core.Entities.Common;

namespace MyBlog.Core.Entities
{
    public class User : EntityBase
    {
        public required string FullName { get; set; }
    }
}