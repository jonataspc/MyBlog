namespace MyBlog.Core.Entities.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        protected EntityBase()
        {
        }
    }
}