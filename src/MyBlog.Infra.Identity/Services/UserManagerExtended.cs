using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBlog.Domain.Bus;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Models.Events;
using MyBlog.Infra.Identity.Models;

namespace MyBlog.Infra.Identity.Services
{
    public class UserManagerExtended : UserManager<ApplicationUser>
    {
        private readonly IGenericMessageBusPublisher _genericMessageBusPublisher;

        public UserManagerExtended(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger, IGenericMessageBusPublisher genericMessageBusPublisher) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _genericMessageBusPublisher = genericMessageBusPublisher;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var result = await base.CreateAsync(user, password);

            // Cria usuario da aplicação relacionado ao ID do AspNetUser registrado pelo Identity
            await _genericMessageBusPublisher.PublishAsync(new UserCreatedEvent(
                new User
                {
                    FullName = user.FullName,
                    Id = Guid.Parse(user.Id)
                })
                );

            return result;
        }
    }
}