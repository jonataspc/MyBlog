using MassTransit;
using MyBlog.Domain.Models.Events;
using MyBlog.Domain.Services;

namespace MyBlog.Application.Bus.Handlers
{
    public class UserCreatedEventHandler(IUserService userService) : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            await userService.AddAsync(context.Message.User);
        }
    }
}