﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBlog.Core.Entities;
using MyBlog.Core.Models;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.Services.Identity
{
    public class UserManagerExtended : UserManager<ApplicationUser>
    {
        private readonly IUserService _userService;

        public UserManagerExtended(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManagerExtended> logger, IUserService userService) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userService = userService;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var result = await base.CreateAsync(user, password);

            // Create an application user based on the user generated by the AspNet Identity
            await _userService.AddAsync(new User
            {
                FullName = user.FullName,
                Id = Guid.Parse(user.Id)
            });

            return result;
        }
    }
}