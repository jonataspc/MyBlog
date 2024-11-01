using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Core.Data.Configurations.Common;
using MyBlog.Core.Entities;

namespace MyBlog.Core.Data.Configurations
{
    internal class CommentEntityConfiguration : EntityBaseConfiguration<Comment>, IEntityTypeConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Content)
                   .HasMaxLength(1024)
                   .IsRequired();

            builder.Property(e => e.IsActive)
                   .HasDefaultValue(true);

            builder.HasQueryFilter(e => e.IsActive);

            builder.HasIndex(e => new { e.IsActive });
            builder.HasIndex(e => new { e.PostId, e.IsActive });

            builder.HasOne(e => e.Post)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(e => e.PostId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}