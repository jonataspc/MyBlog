using MassTransit;
using MyBlog.Domain.Bus;

namespace MyBlog.Application.Bus.Publishers
{
    public class GenericMessageBusPublisherService(IBus bus) : IGenericMessageBusPublisher
    {
        private readonly IBus _bus = bus;

        public async Task PublishAsync(object message)
        {
            await _bus.Publish(message);
        }
    }
}