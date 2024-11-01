namespace MyBlog.Core.Services.Interfaces
{
    public interface IPaginatedList<T> : IEnumerable<T>
    {
        public int PageIndex { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }
    }
}