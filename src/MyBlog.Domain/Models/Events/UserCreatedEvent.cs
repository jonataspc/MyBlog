using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Models.Events
{
    public record UserCreatedEvent(User User);
}