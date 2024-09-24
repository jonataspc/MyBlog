namespace MyBlog.Domain.Data
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}