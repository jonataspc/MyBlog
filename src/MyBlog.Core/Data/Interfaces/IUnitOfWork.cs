namespace MyBlog.Core.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}