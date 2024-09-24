namespace MyBlog.Domain.Bus
{
    public interface IGenericMessageBusPublisher
    {
        Task PublishAsync(object message);
    }
}