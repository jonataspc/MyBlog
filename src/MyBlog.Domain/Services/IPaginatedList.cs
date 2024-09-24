namespace MyBlog.Domain.Services
{
    public interface IPaginatedList<T> : IEnumerable<T>
    {
        public int PageIndex { get; /*private set;*/ }

        public int TotalPages { get; /*private set;*/ }

        public bool HasPreviousPage { get; /*private set;*/ }

        public bool HasNextPage { get; /*private set;*/ }
    }
}