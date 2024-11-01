﻿using MyBlog.Core.Entities.Common;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Core.Entities
{
    public class Post : EntityBase
    {
        public required string Title { get; set; }

        public required string Summary { get; set; }

        public required string Content { get; set; }

        public long ViewCount { get; set; }

        public virtual bool IsActive { get; set; } = true;

        public DateTime PublishDate { get; set; }

        public required Guid AuthorId { get; set; }

        public virtual required Author Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = [];

        public bool AllowEditOrDelete(IAppIdentityUser appIdentityUser)
        {
            return appIdentityUser.IsAdmin() ||
                   appIdentityUser.GetUserId() == Author.UserId;
        }
    }
}