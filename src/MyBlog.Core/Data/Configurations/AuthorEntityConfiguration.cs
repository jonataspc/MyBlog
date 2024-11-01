using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Core.Data.Configurations.Common;
using MyBlog.Core.Entities;

namespace MyBlog.Core.Data.Configurations
{
    internal class AuthorEntityConfiguration : EntityBaseConfiguration<Author>, IEntityTypeConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.User)
                   .WithOne()
                   .HasForeignKey<Author>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}